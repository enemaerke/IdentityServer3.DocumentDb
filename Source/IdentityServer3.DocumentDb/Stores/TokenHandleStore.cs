using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;
using IdentityServer3.DocumentDb.Serialization;

namespace IdentityServer3.DocumentDb.Stores
{
    public class TokenHandleStore : AbstractTokenStore<Token, TokenHandleDocument>, ITokenHandleStore
    {
        private readonly IPropertySerializer _propertySerializer;
        private readonly IClientRepository _clientRepository;

        public TokenHandleStore(
            ITokenHandleRepository repository,
            IPropertySerializer propertySerializer,
            IClientRepository clientRepository):base(repository)
        {
            _propertySerializer = propertySerializer;
            _clientRepository = clientRepository;
        }

        protected override async Task<Token> Convert(TokenHandleDocument document)
        {
            if (document == null)
                return null;

            var client = await _clientRepository.GetByClientId(document.ClientId);
            var result = new Token()
            {
                Audience = document.Audience,
                Claims = await _propertySerializer.Deserialize<List<Claim>>(document.ClaimsListJson),
                Client = client.ToModel(),
                CreationTime = document.CreationTime,
                Issuer = document.Issuer,
                Lifetime = document.Lifetime,
                Type = document.Type,
                Version = document.Version,
            };
            return result;
        }

        public async Task StoreAsync(string key, Token value)
        {
            await Repository.Store(new TokenHandleDocument()
            {
                CreationTime = value.CreationTime,
                Lifetime = value.Lifetime,
                Type = value.Type,
                ClientId = value.ClientId,
                Version = value.Version,
                Audience = value.Audience,
                Issuer = value.Issuer,
                ClaimsListJson = await _propertySerializer.Serialize(value.Claims),
                Key = key,
                Expiry = value.CreationTime.AddSeconds(value.Lifetime),
            });
        }
    }
}