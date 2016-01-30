using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories.Impl;
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
        }

        [Test]
        public async Task CanAddConsent()
        {
            var consent = ObjectMother.CreateConsent();
            var result = await _repo.AddConsent(consent);
            Assert.NotNull(result);
        }
    }
}