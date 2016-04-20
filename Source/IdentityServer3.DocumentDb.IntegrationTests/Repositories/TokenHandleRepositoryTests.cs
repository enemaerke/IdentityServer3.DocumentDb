using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Stores;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class TokenHandleRepositoryTests
    {
        private readonly TokenHandleRepository _repo;

        public TokenHandleRepositoryTests()
        {
            _repo = new TokenHandleRepository(ConnectionSettingsFactory.Create());
            RepoUtil.Reset(_repo);
        }

        [Test]
        public async void CanWriteAndReadATokenById()
        {
            TokenHandleDocument doc = new TokenHandleDocument()
            {
                Audience = "aud",
                ClaimsListJson = "claimsjson",
                ClientId = "clientid",
                CreationTime = DateTimeOffset.Now,
                Expiry = DateTimeOffset.Now,
                Issuer = "issuer",
                Key = "key",
                Lifetime = 10,
                SubjectId = "subjectid",
                Type = "type",
                Version = 1
            };
            await _repo.Store(doc);
            var retrieved = await _repo.GetAsync("key");

            Assert.NotNull(retrieved);
            Assert.AreEqual(doc.Audience, retrieved.Audience);
            Assert.AreEqual(doc.ClaimsListJson, retrieved.ClaimsListJson);
            Assert.AreEqual(doc.ClientId, retrieved.ClientId);
            Assert.AreEqual(doc.CreationTime, retrieved.CreationTime);
            Assert.AreEqual(doc.Expiry, retrieved.Expiry);
            Assert.AreEqual(doc.Issuer, retrieved.Issuer);
            Assert.AreEqual(doc.Key, retrieved.Key);
            Assert.AreEqual(doc.Lifetime, retrieved.Lifetime);
            Assert.AreEqual(doc.SubjectId, retrieved.SubjectId);
            Assert.AreEqual(doc.Type, retrieved.Type);
            Assert.AreEqual(doc.Version, retrieved.Version);

        }
    }
}
