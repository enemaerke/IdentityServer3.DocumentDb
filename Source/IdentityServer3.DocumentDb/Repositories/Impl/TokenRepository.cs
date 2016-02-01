using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Util;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class TokenRepository<TInternal> : RepositoryBase, ITokenRepository<TInternal>
        where TInternal : TokenDocument
    {
        public TokenRepository(ConnectionSettings connectionSettings) :
            base(ReflectionUtil.GetAttributeValue<TInternal, CollectionNameAttribute,string>(a => a.Name), connectionSettings)
        {
        } 

        public async Task<System.Collections.Generic.IEnumerable<TInternal>> GetAllAsync(string subject)
        {
            return await QueryAsync<TInternal>(x => x.SubjectId == subject);
        }

        public async Task<TInternal> GetAsync(string key)
        {
            return await GetDocumentAsync<TInternal>(x => x.Key == key);
        }

        public async Task RemoveAsync(string key)
        {
            await base.DeleteById(key);
        }

        public async Task RevokeAsync(string subject, string client)
        {
            var toBeDeleted = await QueryAsync<TInternal>(x => x.ClientId == client && x.SubjectId == subject);
            foreach (var item in toBeDeleted)
                await base.DeleteById(item.Key);
        }

        public async Task Store(TInternal store)
        {
            await base.Upsert(store);
        }
    }
}