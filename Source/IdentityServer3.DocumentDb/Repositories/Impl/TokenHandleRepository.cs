using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class TokenHandleRepository : TokenRepository<TokenHandleDocument>, ITokenHandleRepository
    {
        public TokenHandleRepository(ICollectionNameResolver resolver, ConnectionSettings connectionSettings) 
            : base(resolver.TokenHandleCollectionName, DocumentTypeNames.TokenHandle, connectionSettings)
        {
        }
    }
}