using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories
{
    public interface IClientRepository
    {
        Task<ClientDocument> GetByClientId(string clientId);
        Task<IEnumerable<ClientDocument>> GetAllClients();
    }
}
