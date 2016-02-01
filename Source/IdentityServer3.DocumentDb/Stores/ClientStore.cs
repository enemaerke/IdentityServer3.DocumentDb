using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;

namespace IdentityServer3.DocumentDb.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IClientRepository _repo;

        public ClientStore(IClientRepository repo)
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

    //internal static class Mappings
    //{
    //    internal static Client ToModel(this ClientDocument clientDocument)
    //    {
    //        if (clientDocument == null)
    //            return null;

    //        return new Client()
    //        {
    //            AbsoluteRefreshTokenLifetime = clientDocument.AbsoluteRefreshTokenLifetime,
    //            ClientId = clientDocument.ClientId,
    //            Claims = clientDocument.Claims.Select(x => x.ToModel()).ToList(),
    //            AuthorizationCodeLifetime = clientDocument.AuthorizationCodeLifetime,
    //            AccessTokenLifetime = clientDocument.AccessTokenLifetime,
    //            AccessTokenType = clientDocument.AccessTokenType,
    //            AllowAccessToAllCustomGrantTypes = clientDocument.AllowAccessToAllGrantTypes,
    //            AllowAccessToAllScopes = clientDocument.AllowAccessToAllScopes,
    //            AllowClientCredentialsOnly = clientDocument.AllowClientCredentialsOnly,
    //            AllowRememberConsent = clientDocument.AllowRememberConsent,
    //            AllowedCorsOrigins = clientDocument.AllowedCorsOrigins.Select(x => x.ToModel()).ToList(),
    //            AllowedCustomGrantTypes = clientDocument.AllowedCustomGrantTypes.Select(x => x.ToModel()).ToList(),
    //            AllowedScopes = clientDocument.AllowedScopes.Select(x => x.ToModel()).ToList(),
    //            AlwaysSendClientClaims = clientDocument.AlwaysSendClientClaims,
    //            ClientName = clientDocument.ClientName,
    //        }
    //    }
    //}
}
