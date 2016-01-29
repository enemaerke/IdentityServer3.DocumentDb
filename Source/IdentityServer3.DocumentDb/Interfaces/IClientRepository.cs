using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Interfaces
{
    public interface IClientRepository
    {
        Task<ClientDocument> GetByClientId(string clientId);
    }
}
