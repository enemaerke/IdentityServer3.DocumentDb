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

namespace IdentityServer3.DocumentDb.Repositories
{
    public class CollectionBase<T>
    {
        private readonly string _dbId;
        private readonly string _collectionName;
        protected CollectionBase(string collectionName, ConnectionSettings settings)
        {
            _dbId = settings.DatabaseId;
            _collectionName = collectionName;

            CreateReliableClient(settings);
            EnsureDatabaseAndCollectionExists().Wait(); 
        }

        protected IReliableReadWriteDocumentClient Client { get; private set; }
        protected DocumentCollection Collection { get; private set; }

        private void CreateReliableClient(ConnectionSettings settings)
        {
            var client = new DocumentClient(new Uri(settings.EndpointUrl), settings.AuthorizationKey, new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });
            var documentRetryStrategy = new DocumentDbRetryStrategy(RetryStrategy.DefaultExponential) { FastFirstRetry = true };
            Client = client.AsReliable(documentRetryStrategy);
        }

        private async Task EnsureDatabaseAndCollectionExists()
        {
            if (Collection != null)
                return;

            var client =Client;
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == _dbId).AsEnumerable().FirstOrDefault();

            // If the database does not exist, create a new database
            if (database == null)
            {
                database = await client.CreateDatabaseAsync(
                    new Database
                    {
                        Id = _dbId
                    });
            }
            Collection = client.CreateDocumentCollectionQuery("dbs/" + _dbId).AsEnumerable()
                .FirstOrDefault(c => c.Id == _collectionName);

            // If the document collection does not exist, create a new collection
            if (Collection == null)
            {
                Collection = await client.CreateDocumentCollectionAsync("dbs/" + _dbId,
                    new DocumentCollection
                    {
                        Id = _collectionName
                    });

            }
        }

        protected IQueryable<T> GetDocumentQuery()
        {
            return Client.CreateDocumentQuery<T>(Collection.DocumentsLink);
        }

        protected async Task<T> GetFirstAsync(Expression<Func<T, bool>> expression)
        {
            var query = GetDocumentQuery();
            query = query.Where(expression);
            var result = await QueryAsync(query);

            return result.FirstOrDefault();
        }

        protected async Task<IEnumerable<T>> QueryAsync(IQueryable<T> query)
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
    }
}