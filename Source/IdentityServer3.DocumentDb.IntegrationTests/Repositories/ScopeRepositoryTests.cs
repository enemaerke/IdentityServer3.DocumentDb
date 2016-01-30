using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories.Impl;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class ScopeRepositoryTests
    {
        private readonly ScopeRepository _repo;

        public ScopeRepositoryTests()
        {
            _repo = new ScopeRepository(ConnectionSettingsFactory.Create());
        }

        [Test]
        public async Task CanQueryWithoutExceptions()
        {
            var result = await _repo.GetByScopeNames(new[] {"some", "scope"});
            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task CanQueryExistingScopes()
        {
            await _repo.AddScope(ObjectMother.CreateScope("myscope"));

            var result = await _repo.GetByScopeNames(new[] {"myscope", "shouldnotbefound"});
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Count());
        }
    }
}