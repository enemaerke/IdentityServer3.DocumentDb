using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Client.TransientFaultHandling;
using Microsoft.Azure.Documents.Client.TransientFaultHandling.Strategies;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class CollectionBase
    {
        private readonly string _collectionName;
        protected IReliableReadWriteDocumentClient _client;
        protected DocumentCollection _collection;
        protected readonly string _dbId;

        protected CollectionBase(string collectionName, DocumentDbConnectionSettings settings)
        {
            _dbId = settings.DatabaseId;
            _collectionName = collectionName;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            CreateReliableClient(settings);
        }

        private void CreateReliableClient(DocumentDbConnectionSettings settings)
        {
            var client = new DocumentClient(new Uri(settings.EndpointUrl), settings.AuthorizationKey, new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });
            var documentRetryStrategy = new DocumentDbRetryStrategy(RetryStrategy.DefaultExponential) { FastFirstRetry = true };
            _client = client.AsReliable(documentRetryStrategy);
        }

        public async Task CreateDatabaseIfNotExist()
        {
            Database database = _client.CreateDatabaseQuery().Where(db => db.Id == _dbId).AsEnumerable().FirstOrDefault();

            // If the database does not exist, create a new database
            if (database == null)
            {
                database = await _client.CreateDatabaseAsync(
                    new Database
                    {
                        Id = _dbId
                    });
            }
        }

        public async Task CreateCollectionIfNotExists()
        {
            if (_collection != null)
                return;

            _collection = _client.CreateDocumentCollectionQuery("dbs/" + _dbId).AsEnumerable()
                .FirstOrDefault(c => c.Id == _collectionName);

            // If the document collection does not exist, create a new collection
            if (_collection == null)
            {
                _collection = await _client.CreateDocumentCollectionAsync("dbs/" + _dbId,
                    new DocumentCollection
                    {
                        Id = _collectionName
                    });

            }
        }

        protected Document GetDocument(string id)
        {
            return GetDocument<Document>(d => d.Id == id);
        }

        protected IQueryable<TDoc> GetDocumentQuery<TDoc>()
        {
            return _client.CreateDocumentQuery<TDoc>(_collection.DocumentsLink);
        }

        protected TDoc GetDocument<TDoc>(Expression<Func<TDoc, bool>> whereClause)
        {
            return GetDocumentQuery<TDoc>()
                .Where(whereClause)
                .AsEnumerable()
                .FirstOrDefault();
        }

        protected async Task<TDoc> GetByExpression<TDoc>(Expression<Func<TDoc, bool>> expression)
        {
            var query = GetDocumentQuery<TDoc>()
                .Where(expression);
            var result = await QueryAsync(query);

            return result.FirstOrDefault();
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(IQueryable<T> query)
        {
            var docQuery = query.AsDocumentQuery();
            var batches = new List<IEnumerable<T>>();

            do
            {
                var batch = await docQuery.ExecuteNextAsync<T>();

                batches.Add(batch);
            }
            while (docQuery.HasMoreResults);

            var docs = batches.SelectMany(b => b);

            return docs;
        }

        protected bool Exists(string id)
        {
            return GetDocument(id) != null;
        }

        protected bool NotExist(string id)
        {
            return GetDocument(id) == null;
        }
    }
}