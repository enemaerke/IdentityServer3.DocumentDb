using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Extensions;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class DocumentDbNames
    {
        public const string ClientCollectionName = "client";
        public const string ScopeCollectionName = "scope";
    }

    public class ClientConfigurationRepository : CollectionBase<Client>, IClientConfigurationRepository
    {
        public ClientConfigurationRepository(ConnectionSettings settings) : base(DocumentDbNames.ClientCollectionName, settings)
        {
        }

        public async Task<Client> GetByClientId(string clientId)
        {
            return await base.GetFirstAsync(x => x.ClientId == clientId);
        }
    }

    public class ScopeConfigurationRepository : CollectionBase<Scope>, IScopeConfigurationRepository
    {
        public ScopeConfigurationRepository(ConnectionSettings setting):base(DocumentDbNames.ScopeCollectionName, setting) { }

        public async Task<IEnumerable<Scope>> GetByScopeNames(string[] scopeNames)
        {
            //TODO: sql injection sanitized somehow?
            string collectionName = DocumentDbNames.ScopeCollectionName;
            string namesSerialized = scopeNames.JoinToString(",", str => $"'str'");

            var query = Client.CreateDocumentQuery<Scope>(Collection.DocumentsLink,  
                $"SELECT * FROM ${collectionName} WHERE ${collectionName}.Name IN ({namesSerialized})");
            return await base.QueryAsync(query);
        }
    }
}
