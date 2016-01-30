using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories
{
    public interface IScopeRepository
    {
        Task<IEnumerable<ScopeDocument>> GetByScopeNames(string[] scopeNames);
    }
}