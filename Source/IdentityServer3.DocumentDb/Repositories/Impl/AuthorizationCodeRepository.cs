using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class AuthorizationCodeRepository : TokenRepository<AuthorizationCodeTokenDocument>, IAuthorizationCodeRepository
    {
        public AuthorizationCodeRepository(ConnectionSettings connectionSettings) : base(connectionSettings)
        {
        }
    }
}