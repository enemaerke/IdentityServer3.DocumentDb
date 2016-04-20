using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class ClientRepository : RepositoryBase<ClientDocument>, IClientRepository
    {
        public ClientRepository(ICollectionNameResolver resolver, ConnectionSettings settings) : base(resolver.ClientCollectionName, DocumentTypeNames.Client, settings)
        {
        }

        public async Task<ClientDocument> GetByClientId(string clientId)
        {
            return await base.GetDocumentAsync(x => x.ClientId == clientId);
        }

        public async Task<IEnumerable<ClientDocument>> GetAllClients()
        {
            return await base.GetAll();
        }

        public async Task<ClientDocument> AddClient(ClientDocument client)
        {
            return await base.Upsert(client);
        }

        public async Task<bool> DeleteClientById(string id)
        {
            return await base.DeleteById(id);
        }
    }
}
