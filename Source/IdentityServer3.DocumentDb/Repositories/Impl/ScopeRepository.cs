using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class ScopeRepository : CollectionBase, IScopeRepository
    {
        public ScopeRepository(ConnectionSettings setting):base(DocumentDbNames.ScopeCollectionName, setting) { }

        public async Task<IEnumerable<ScopeDocument>> GetByScopeNames(string[] scopeNames)
        {
            //TODO: sql injection sanitized somehow?
            string collectionName = DocumentDbNames.ScopeCollectionName;
            string namesSerialized = string.Join(", ", scopeNames.Select(s => $"'{s}'"));

            var query = base._client.CreateDocumentQuery<ScopeDocument>(_collection.DocumentsLink,  
                $"SELECT * FROM ${collectionName} WHERE ${collectionName}.Name IN ({namesSerialized})");
            return await base.QueryAsync(query);
        }
    }
}