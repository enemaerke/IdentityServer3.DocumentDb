using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IClientConfigurationRepository _repo;

        public ClientStore(IClientConfigurationRepository repo)
        {
            _repo = repo;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var entity = await _repo.GetByClientId(clientId);
            var client = entity.ToModel();
            return client;
        }
    }
}
