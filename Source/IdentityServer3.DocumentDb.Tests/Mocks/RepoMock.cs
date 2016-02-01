using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;

namespace IdentityServer3.DocumentDb.Tests.Mocks
{
    public class RepoMock<T>
    {
        public List<T> List { get; } = new List<T>();
    }

    public class MockScopeRepository : RepoMock<ScopeDocument>, IScopeRepository
    {
        public Task<IEnumerable<ScopeDocument>> GetByScopeNames(string[] scopeNames)
        {
            return Task.FromResult(List.Where(x => scopeNames.Contains(x.Name)));
        }
    }

    public class MockClientRepository : RepoMock<ClientDocument>, IClientRepository
    {
        public Task<ClientDocument> GetByClientId(string clientId)
        {
            return Task.FromResult(List.FirstOrDefault(x => x.ClientId == clientId));
        }

        public Task<IEnumerable<ClientDocument>> GetAllClients()
        {
            return Task.FromResult(List.AsEnumerable());
        }
    }
}
