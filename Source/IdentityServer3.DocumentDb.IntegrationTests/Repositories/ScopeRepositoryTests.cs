using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Tests;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class ScopeRepositoryTests
    {
        private readonly ScopeRepository _repo;

        public ScopeRepositoryTests()
        {
            _repo = new ScopeRepository(TestFactory.SharedCollection, TestFactory.CreateConnectionSettings());
            RepoUtil.Reset(_repo);
        }

        [Test]
        public async Task CanQueryWithoutExceptions()
        {
            var result = await _repo.GetByScopeNames(new[] {"some", "scope"});
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task CanGetAll()
        {
            RepoUtil.Reset(_repo);

            await _repo.AddScope(ObjectMother.CreateScopeDocument("scope1"));
            await _repo.AddScope(ObjectMother.CreateScopeDocument("scope2"));
            await _repo.AddScope(ObjectMother.CreateScopeDocument("scope3"));

            var result = await _repo.GetAllScopes();
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public async Task CanQueryExistingScopes()
        {
            await _repo.AddScope(ObjectMother.CreateScopeDocument("myscope"));

            var result = await _repo.GetByScopeNames(new[] {"myscope", "shouldnotbefound"});
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task CanTestIfEmpty()
        {
            RepoUtil.Reset(_repo);
            var isEmpty = await _repo.IsEmpty();
            Assert.True(isEmpty);

            await _repo.AddScope(ObjectMother.CreateScopeDocument("myscope"));
            isEmpty = await _repo.IsEmpty();
            Assert.False(isEmpty);
        }
    }
}