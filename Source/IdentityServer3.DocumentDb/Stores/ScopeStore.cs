using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Interfaces;
using IdentityServer3.DocumentDb.Repositories;

namespace IdentityServer3.DocumentDb.Stores
{
    public class ScopeStore : IScopeStore
    {
        private readonly IScopeConfigurationRepository _scopeConfigurationRepository;
        public ScopeStore(IScopeConfigurationRepository scopeConfigurationRepository)
        {
            _scopeConfigurationRepository = scopeConfigurationRepository;
        }

        public async Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            var scopeConfigs = await _scopeConfigurationRepository.GetByScopeNames(scopeNames.ToArray());
            return scopeConfigs.Select(x => EntitiesMap.ToModel(x));
        }

        public Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            throw new NotImplementedException();
        }
    }
}