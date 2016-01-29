using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Stores
{
    public class RefreshTokenStore : AbstractTokenStore<RefreshToken, RefreshTokenDocument>, IRefreshTokenStore
    {
        private readonly IPropertySerializer _propertySerializer;

        public RefreshTokenStore(
            ITokenRepository<RefreshTokenDocument> repository,
            IPropertySerializer propertySerializer) : base(repository)
        {
            _propertySerializer = propertySerializer;
        }

        protected override async Task<RefreshToken> Convert(RefreshTokenDocument document)
        {
            if (document == null)
                return null;

            return new RefreshToken()
            {
                AccessToken = await _propertySerializer.Deserialize<Token>(document.AccessTokenJson),
                Version = document.Version,
                CreationTime = document.CreationTime,
                LifeTime = document.LifeTime,
                Subject = await _propertySerializer.Deserialize<ClaimsPrincipal>(document.SubjectJson),
            };
        }

        public async Task StoreAsync(string key, RefreshToken value)
        {
            await Repository.Store(new RefreshTokenDocument()
            {
                ClientId = value.ClientId,
                Key = key,
                SubjectId = value.SubjectId,
                AccessTokenJson = await _propertySerializer.Serialize(value.AccessToken),
                CreationTime = value.CreationTime,
                LifeTime = value.LifeTime,
                Version = value.Version,
                SubjectJson = await _propertySerializer.Serialize(value.Subject),
                Expiry = value.CreationTime.AddSeconds(value.LifeTime),
            });
        }
    }
}