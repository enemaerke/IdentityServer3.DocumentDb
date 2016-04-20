using System;
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
    public class AuthorizationCodeStore : AbstractTokenStore<AuthorizationCode, AuthorizationCodeTokenDocument>,
        IAuthorizationCodeStore
    {
        private readonly IClientRepository _clientRepository;
        private readonly IPropertySerializer _propertySerializer;

        public AuthorizationCodeStore(
            IAuthorizationCodeRepository repository,
            IClientRepository clientRepository,
            IPropertySerializer propertySerializer) : base(repository)
        {
            _clientRepository = clientRepository;
            _propertySerializer = propertySerializer;
        }

        protected override async Task<AuthorizationCode> Convert(AuthorizationCodeTokenDocument document)
        {
            if (document == null)
                return null;

            return new AuthorizationCode()
            {
                Client = (await _clientRepository.GetByClientId(document.ClientId)).ToModel(),
                CreationTime = document.CreationTimeSecondsSinceEpoch.FromEpoch(),
                IsOpenId = document.IsOpenId,
                Nonce = document.Nonce,
                RedirectUri = document.RedirectUri,
                RequestedScopes =
                    await _propertySerializer.Deserialize<IEnumerable<Scope>>(document.RequestScopesJson),
                SessionId = document.SessionId,
                WasConsentShown = document.WasConsentShown,
                Subject = await _propertySerializer.Deserialize<ClaimsPrincipal>(document.SubjectJson),
            };
        }

        public async Task StoreAsync(string key, AuthorizationCode value)
        {
            if (value == null)
                return;

            await Repository.Store(new AuthorizationCodeTokenDocument()
            {
                ClientId = value.ClientId,
                CreationTimeSecondsSinceEpoch = value.CreationTime.ToEpoch(),
                Id = key,
                SubjectId = value.SubjectId,
                Nonce = value.Nonce,
                RedirectUri = value.RedirectUri,
                IsOpenId = value.IsOpenId,
                SubjectJson = await _propertySerializer.Serialize(value.Subject),
                WasConsentShown = value.WasConsentShown,
                SessionId = value.SessionId,
                RequestScopesJson = await _propertySerializer.Serialize(value.RequestedScopes),
                Expiry = DateTimeOffset.UtcNow.AddSeconds(value.Client.AuthorizationCodeLifetime),
            });
        }
    }
}