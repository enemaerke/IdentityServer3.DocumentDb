using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;

namespace IdentityServer3.DocumentDb.Services
{
    public class ClientCorsPolicyService : ICorsPolicyService
    {
        private readonly IClientRepository _repository;
        public ClientCorsPolicyService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var clients = await _repository.GetAllClients();
            
            var origins = clients.SelectMany(x => x.AllowedCorsOrigins).Select(x => x.GetOrigin()).Where(x => x != null).Distinct();

            var result = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            return result;
        }
    }

    internal static class StringExtensions
    {
        public static string GetOrigin(this ClientCorsOrigin clientCorsOrigin)
        {
            return GetOrigin(clientCorsOrigin.Origin);
        }
        public static string GetOrigin(this string url)
        {
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                var idx = url.IndexOf("//");
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }
    }

}
