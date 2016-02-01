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

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class RepositoryBase 
    {
        protected string CollectionName { get; private set; }
        protected IReliableReadWriteDocumentClient Client { get; private set; }
        protected DocumentCollection Collection { get; private set; }
        protected string DatabaseId { get; private set; } 

        protected RepositoryBase(string collectionName, ConnectionSettings settings)
        {
            DatabaseId = settings.DatabaseId;
            CollectionName = collectionName;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            CreateReliableClient(settings);
            CreateCollectionIfNotExists().Wait();
        }

        private void CreateReliableClient(ConnectionSettings settings)
        {
            var client = new DocumentClient(new Uri(settings.EndpointUrl), settings.AuthorizationKey, new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });
            var documentRetryStrategy = new DocumentDbRetryStrategy(RetryStrategy.DefaultExponential) {FastFirstRetry = true};
            Client = client.AsReliable(documentRetryStrategy);
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

        protected async Task<Document> GetDocument(string id)
        {
            var doc = await GetDocumentAsync<Document>(d => d.Id == id);
            return doc;
        }

        protected async Task<T> GetById<T>(string id)
        {
            // COMMENTED OUT, THIS SEEMED LIKE A NEATER SOLUTION BUT FAILS IF RESOURCE DOES NOT EXIST
            // cast needed: http://stackoverflow.com/a/27288059/9222
            // see request for better API: https://github.com/Azure/azure-documentdb-dotnet/issues/71
            //var uri = UriFactory.CreateDocumentUri(DatabaseId, Collection.Id, id);
            //var r = await Client.ReadDocumentAsync(uri, new RequestOptions());
            //return (T)(dynamic)r.Resource;

            var docQuery = Client.CreateDocumentQuery<T>(Collection.DocumentsLink,
                new SqlQuerySpec()
                {
                    QueryText = $"SELECT * FROM {CollectionName} x WHERE x.id = @id",
                    Parameters = new SqlParameterCollection()
                    {
                        new SqlParameter("@id", id),
                    }
                });
            return await QueryFirstAsync(docQuery);
        }

        protected TDoc GetDocument<TDoc>(Expression<Func<TDoc,bool>> whereClause)
        {
            return Client.CreateDocumentQuery<TDoc>(Collection.DocumentsLink)
                .Where(whereClause)
                .AsEnumerable()
                .FirstOrDefault();
        }

        protected async Task<TDoc> GetDocumentAsync<TDoc>(Expression<Func<TDoc, bool>> whereClause)
        {
            var query = Client.CreateDocumentQuery<TDoc>(Collection.DocumentsLink)
                .Where(whereClause);
            return await QueryFirstAsync(query);
        }

        protected async Task<IEnumerable<TDoc>> GetAll<TDoc>()
        {
            var query = Client.CreateDocumentQuery<TDoc>(Collection.DocumentsLink);
            return await QueryAsync(query);
        }

        protected async Task<T> QueryFirstAsync<T>(IQueryable<T> query)
        {
            var docQuery = query.AsDocumentQuery();
            var result = await docQuery.ExecuteNextAsync<T>();
            return result.AsEnumerable().FirstOrDefault();
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(Expression<Func<T,bool>> whereClause)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink)
                .Where(whereClause);
            return await QueryAsync(query);
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

        protected bool Exists(string id)
        {
            return GetDocument(id).Result != null;
        }

        protected bool NotExist(string id)
        {
            return GetDocument(id).Result == null;
        }

        protected async Task<T> Upsert<T>(T entity)
        {
            var response = await Client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, Collection.Id), entity);
            //TODO: check response and raise descriptive exception
            return entity;
        }
    }
}
