using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class RefreshTokenRepository : TokenRepository<RefreshTokenDocument>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ICollectionNameResolver resolver, ConnectionSettings connectionSettings) 
            : base(resolver.RefreshTokenCollectionName, DocumentTypeNames.RefreshToken, connectionSettings)
        {
        }
    }
}