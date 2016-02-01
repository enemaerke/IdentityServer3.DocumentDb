using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class RefreshTokenRepository : TokenRepository<RefreshTokenDocument>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
    }
}