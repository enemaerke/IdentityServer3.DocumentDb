using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Interfaces
{
    public interface IClientConfigurationRepository
    {
        Task<Client> GetByClientId(string clientId);
    }

    public interface IScopeConfigurationRepository
    {
        Task<Scope> GetBy
    }
}
