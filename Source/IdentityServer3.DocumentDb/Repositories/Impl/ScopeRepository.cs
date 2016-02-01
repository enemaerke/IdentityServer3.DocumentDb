using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class ScopeRepository : RepositoryBase, IScopeRepository
    {
        public ScopeRepository(ConnectionSettings setting):base(DocumentDbNames.ScopeCollectionName, setting) { }

        public async Task<IEnumerable<ScopeDocument>> GetByScopeNames(string[] scopeNames)
        {
            //TODO: sql injection sanitized somehow?
            string collectionName = DocumentDbNames.ScopeCollectionName;
            string namesSerialized = string.Join(", ", scopeNames.Select(s => $"'{s}'"));

            string sql = $"SELECT * FROM {collectionName} WHERE {collectionName}.Name IN ({namesSerialized})";
            var query = base.Client.CreateDocumentQuery<ScopeDocument>(Collection.DocumentsLink,  
                sql);
            return await base.QueryAsync(query);
        }

        public async Task AddScope(ScopeDocument scope)
        {
            await base.Upsert(scope);
        }
    }
}