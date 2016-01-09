using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class ClientConfigurationRepository : CollectionBase, IClientConfigurationRepository
    {
        public ClientConfigurationRepository(string collectionName, DocumentDbConnectionSettings settings) : base(collectionName, settings)
        {
        }

        public async Task<Client> GetByClientId(string clientId)
        {
            return await base.GetByExpression<Client>(x => x.ClientId == clientId);
        }
    }

    public class ScopeConfigurationRepository : CollectionBase, IScopeConfigurationRepository
    {
        
    }
}
