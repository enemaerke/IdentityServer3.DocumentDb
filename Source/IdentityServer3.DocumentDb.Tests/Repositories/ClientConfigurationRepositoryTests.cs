using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.Tests.Repositories
{
    [TestFixture]
    public class ClientConfigurationRepositoryTests
    {
        private ClientConfigurationRepository _repo;

        public ClientConfigurationRepositoryTests()
        {
            _repo = new ClientConfigurationRepository(ConnectionSettingsFactory.Create());
        }

        [Test]
        public async void CanLookForUnknownClientWithoutExceptions()
        {
            var result = await _repo.GetByClientId(Guid.NewGuid().ToString());
            Assert.IsNull(result);
        }

        [Test]
        public async void CanQueryExistingClient()
        {
            var client = ObjectMother.CreateClient();
            var added = await _repo.AddClient(client);

            var returned = await _repo.GetByClientId(client.ClientId);
            Assert.NotNull(returned);

            //TODO: assert similar to original

            var deleteResult = await _repo.DeleteClientById(returned.Id);
            Assert.True(deleteResult);
        }
    }

    public class ConsentRepositoryTests
    {
        private ConsentRepository _repo;

        public ConsentRepositoryTests()
        {
            _repo = new ConsentRepository(ConnectionSettingsFactory.Create());
        }

        [Test]
        public async void CanAddConsent()
        {
            var consent = ObjectMother.CreateConsent();
            var result = await _repo.AddConsent(consent);
            Assert.NotNull(result);
        }
    }
}