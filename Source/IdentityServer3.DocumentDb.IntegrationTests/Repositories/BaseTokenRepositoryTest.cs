using System;
using System.Linq;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;
using IdentityServer3.DocumentDb.Repositories.Impl;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    public abstract class BaseTokenRepositoryTest<T, TRepository> 
        where T : TokenDocument, IEquatable<T> 
        where TRepository : ITokenRepository<T>
    {
        private readonly TRepository _repo;
        protected abstract TRepository CreateRepo(ICollectionNameResolver resolver, ConnectionSettings settings);
        protected abstract T CreateDocument(string key, string clientid = null, string subjectid = null);

        protected BaseTokenRepositoryTest()
        {
            _repo = CreateRepo(TestFactory.SharedCollection, TestFactory.CreateConnectionSettings());
            Reset();
        }

        private void Reset()
        {
            RepoUtil.Reset<T>(_repo as RepositoryBase<T>);
        }

        [Test]
        public async void CanWriteAndReadATokenById()
        {
            var doc = CreateDocument("key");
            await _repo.Store(doc);
            var retrieved = await _repo.GetAsync("key");

            Assert.AreEqual(doc, retrieved);

            await _repo.RemoveAsync("key");

            var after = await _repo.GetAsync("key");
            Assert.Null(after);
        }

        [Test]
        public async void CanGetAll()
        {
            Reset();
            await _repo.Store(CreateDocument("xkey1", clientid: "cli1", subjectid:"sub1"));
            await _repo.Store(CreateDocument("xkey2", clientid: "cli2", subjectid: "sub1"));
            await _repo.Store(CreateDocument("xkey3", clientid: "cli1", subjectid: "sub2"));

            var retrieved = await _repo.GetAllAsync("sub1");
            Assert.AreEqual(2, retrieved.Count());
        }

        [Test]
        public async void CanRevoke()
        {
            var doc = CreateDocument("key3", "cli1", "subj1");
            await _repo.Store(doc);
            var retreived = await _repo.GetAsync("key3");
            Assert.AreEqual(doc, retreived);

            await _repo.RevokeAsync("subj1", "cli1");
            var after = await _repo.GetAsync("key3");
            Assert.Null(after);
        }

        [Test]
        public async void CanGetExpired()
        {
            Reset();

            DateTimeOffset now = DateTimeOffset.Now;
            var doc1 = CreateDocument("key1");
            doc1.Expiry = now.Subtract(TimeSpan.FromSeconds(1));
            await _repo.Store(doc1);
            var doc2 = CreateDocument("key2");
            doc2.Expiry = now.Add(TimeSpan.FromSeconds(1));
            await _repo.Store(doc2);

            var retrieved = (await _repo.GetExpired(now)).ToList();
            Assert.AreEqual(1, retrieved.Count);
            Assert.AreEqual(doc1, retrieved.First());
        }
    }
}