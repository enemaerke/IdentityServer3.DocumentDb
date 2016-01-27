using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class ClientConfigurationRepository : CollectionBase, IClientConfigurationRepository
    {
        public ClientConfigurationRepository(ConnectionSettings settings) : base(DocumentDbNames.ClientCollectionName, settings)
        {
        }

        public async Task<ClientDocument> GetByClientId(string clientId)
        {
            return await base.GetDocumentAsync<ClientDocument>(x => x.ClientId == clientId);
        }

        public async Task<ClientDocument> AddClient(ClientDocument client)
        {
            return await base.Upsert<ClientDocument>(client);
        }

        public async Task<bool> DeleteClientById(string id)
        {
            return await base.DeleteById(id);
        }
    }
}
