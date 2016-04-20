using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Util;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class TokenRepository<TInternal> : RepositoryBase<TInternal>, ITokenRepository<TInternal>
        where TInternal : TokenDocument
    {
        public TokenRepository(string collectionName, string documentType, ConnectionSettings connectionSettings) :
            base(collectionName, documentType, connectionSettings)
        {
        } 

        public async Task<System.Collections.Generic.IEnumerable<TInternal>> GetAllAsync(string subject)
        {
            return await QueryAsync(x => x.SubjectId == subject);
        }

        public async Task<TInternal> GetAsync(string key)
        {
            return await GetDocumentAsync(x => x.Id == key);
        }

        public async Task RemoveAsync(string key)
        {
            await base.DeleteById(key);
        }

        public async Task RevokeAsync(string subject, string client)
        {
            var toBeDeleted = await QueryAsync(x => x.ClientId == client && x.SubjectId == subject);
            foreach (var item in toBeDeleted)
                await base.DeleteById(item.Id);
        }

        public async Task Store(TInternal store)
        {
            await base.Upsert(store);
        }

        public async Task<IEnumerable<TInternal>> GetExpired(DateTimeOffset expiryDate)
        {
            var epochSeconds = expiryDate.ToEpoch();
            return await QueryAsync(x => x.ExpirySecondsSinceEpoch < epochSeconds);
        }
    }
}