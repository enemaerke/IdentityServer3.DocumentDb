using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories
{
    public interface ITokenRepository<TInternal>
        where TInternal : TokenDocument
    {
        Task<TInternal> GetAsync(string key);
        Task RemoveAsync(string key);
        Task<IEnumerable<TInternal>> GetAllAsync(string subject);
        Task RevokeAsync(string subject, string client);
        Task Store(TInternal store);
    }

    public interface ITokenHandleRepository : ITokenRepository<TokenHandleDocument> { }

    public interface IAuthorizationCodeRepository : ITokenRepository<AuthorizationCodeTokenDocument> { }

    public interface IRefreshTokenRepository : ITokenRepository<RefreshTokenDocument> { }
}