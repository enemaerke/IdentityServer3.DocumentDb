using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;

namespace IdentityServer3.DocumentDb.Stores
{
    public abstract class AbstractTokenStore<TReturn, TInternal> 
        where TReturn: class, ITokenMetadata
        where TInternal:TokenDocument
    {
        protected ITokenRepository<TInternal> Repository { get; }

        protected AbstractTokenStore(ITokenRepository<TInternal> repository)
        {
            Repository = repository;
        }

        protected abstract Task<TReturn> Convert(TInternal document);

        public async Task<TReturn> GetAsync(string key)
        {
            var token = await Repository.GetAsync(key);
            if (token != null && token.Expiry < DateTimeOffset.UtcNow)
                return null;

            return await Convert(token);
        }

        public async Task RemoveAsync(string key)
        {
            await Repository.RemoveAsync(key);
        }

        public async Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            var all = await Repository.GetAllAsync(subject);
            List<TReturn> list = new List<TReturn>();
            foreach (var a in all)
            {
                list.Add(await Convert(a));
            }
            return list.Cast<ITokenMetadata>();
        }

        public async Task RevokeAsync(string subject, string client)
        {
            await Repository.RevokeAsync(subject, client);
        }
    }
}