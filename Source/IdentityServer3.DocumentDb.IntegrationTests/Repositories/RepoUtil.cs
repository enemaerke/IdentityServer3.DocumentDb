using System.Collections.Generic;
using IdentityServer3.DocumentDb.Repositories.Impl;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    public static class RepoUtil
    {
        public static void Reset<T>(RepositoryBase<T> repo)
        {
            var getAllQuery = repo.Client.CreateDocumentQuery(repo.Collection.DocumentsLink);
            var docQuery = getAllQuery.AsDocumentQuery();
            var idList = new List<string>();

            do
            {
                var batch = docQuery.ExecuteNextAsync().Result;

                foreach (var item in batch)
                {
                    string id = ((dynamic) item).id;
                    idList.Add(id);
                }
            }
            while (docQuery.HasMoreResults);

            foreach (var id in idList)
            {
                var uri = UriFactory.CreateDocumentUri(repo.DatabaseId, repo.Collection.Id, id);
                repo.Client.DeleteDocumentAsync(uri).Wait();
            }
        }
    }
}