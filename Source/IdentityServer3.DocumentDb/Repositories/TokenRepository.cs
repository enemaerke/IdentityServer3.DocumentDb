using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Interfaces;
using IdentityServer3.DocumentDb.Util;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class TokenRepository<TInternal> : CollectionBase, ITokenRepository<TInternal>
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
            return await base.DeleteById(sddg);
        }

        public async Task RevokeAsync(string subject, string client)
        {

            await base.DeleteBy(X500DistinguishedName => )
        }

        public async Task Store(TInternal store)
        {
            await base.Upsert(store);
        }
    }
}