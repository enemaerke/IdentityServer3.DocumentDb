using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Client.TransientFaultHandling;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class RepositoryBase<T> where T:DocumentBase
    {
        private readonly string _documentType;
        public string CollectionName { get; }
        public IReliableReadWriteDocumentClient Client { get; private set; }
        public DocumentCollection Collection { get; private set; }
        public string DatabaseId { get; } 

        protected RepositoryBase(string collectionName, string documentType, ConnectionSettings settings)
        {
            _documentType = documentType;
            DatabaseId = settings.DatabaseId;
            CollectionName = collectionName;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            Client = RepositoryHelper.CreateClient(settings);
            CreateCollectionIfNotExists().Wait();
        }

        private async Task CreateDatabaseIfNotExist()
        {
            Database database = Client.CreateDatabaseQuery()
                .Where(db => db.Id == DatabaseId)
                .AsEnumerable()
                .FirstOrDefault();
            
            // If the database does not exist, create a new database
            if (database == null)
            {
                database = await Client.CreateDatabaseAsync(
                    new Database
                    {
                        Id = DatabaseId
                    });
            }
        }

        private async Task CreateCollectionIfNotExists()
        {
            if (Collection != null)
                return;

            await CreateDatabaseIfNotExist();

            Collection = Client.CreateDocumentCollectionQuery("dbs/" + DatabaseId).AsEnumerable()
                .FirstOrDefault(c => c.Id == CollectionName);

            // If the document collection does not exist, create a new collection
            if (Collection == null)
            {
                Collection = await Client.CreateDocumentCollectionAsync("dbs/" + DatabaseId,
                    new DocumentCollection
                    {
                        Id = CollectionName
                    });
                
            }
        }

        protected async Task<T> GetById(string id)
        {
            var docQuery = Client.CreateDocumentQuery<T>(Collection.DocumentsLink).Where(x => x.Id == id && x.DocType == _documentType);
            return await QueryFirstAsync(docQuery);
        }

        protected async Task<T> GetDocumentAsync(Expression<Func<T, bool>> whereClause)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(x => x.DocType == _documentType)
                .Where(whereClause);
            return await QueryFirstAsync(query);
        }

        protected async Task<IEnumerable<T>> GetAll()
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink).Where(x => x.DocType == _documentType);
            return await QueryAsync(query);
        }

        private async Task<T> QueryFirstAsync(IQueryable<T> query)
        {
            var docQuery = query.AsDocumentQuery();
            var result = await docQuery.ExecuteNextAsync<T>();
            return result.AsEnumerable().FirstOrDefault();
        }

        protected async Task<IEnumerable<T>> QueryAsync(Expression<Func<T,bool>> whereClause)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(x => x.DocType == _documentType)
                .Where(whereClause);
            return await QueryAsync(query);
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

        protected async Task<bool> DeleteById(string id, bool throwIfNotSuccessfull = false)
        {
            var uri = UriFactory.CreateDocumentUri(DatabaseId, Collection.Id, id);
            var response = await Client.DeleteDocumentAsync(uri);
            int statusCode = (int) response.StatusCode;
            bool successfull = 200 <= statusCode && statusCode < 300;
            if (!successfull && throwIfNotSuccessfull)
                throw new InvalidOperationException($"Could not remove DocDb document on id: {id}, httpstatus code was: {response.StatusCode}");
            return successfull;
        }

        protected async Task<T> Upsert(T entity)
        {
            var response = await Client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, Collection.Id), entity);
            //TODO: check response and raise descriptive exception
            return entity;
        }

        public async Task<bool> IsEmpty()
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .AsDocumentQuery();
            var result = await query.ExecuteNextAsync<T>();
            return result.Count == 0;
        }
    }
}
