using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Tests;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class ConsentRepositoryTests
    {
        private readonly ConsentRepository _repo;

        public ConsentRepositoryTests()
        {
            _repo = new ConsentRepository(ConnectionSettingsFactory.Create());
            RepoUtil.Reset(_repo);
        }

        [Test]
        public async Task CanAddConsent()
        {
            var consent = ObjectMother.CreateConsentDocument();
            var result = await _repo.AddConsent(consent);
            Assert.NotNull(result);
        }
    }
}