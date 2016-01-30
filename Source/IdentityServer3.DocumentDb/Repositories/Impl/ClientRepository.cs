using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class ClientRepository : CollectionBase, IClientRepository
    {
        public ClientRepository(ConnectionSettings settings) : base(DocumentDbNames.ClientCollectionName, settings)
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
