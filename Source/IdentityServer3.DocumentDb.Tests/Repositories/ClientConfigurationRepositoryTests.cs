using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories;
using NUnit.Framework;
using Client = IdentityServer3.DocumentDb.Entities.Client;

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

        public async void CanQueryExistingClient()
        {
            var added = await _repo.AddClient(
            })
        }
    }

    public class ObjectMother
    {
        private static readonly Random s_random = new Random();
        private static int NewInt32()
        {
            return s_random.Next(1);
        }
        public Client Create(string id = null)
        {
            id = id ?? Guid.NewGuid().ToString();
            return new Client()
            {
                ClientId = Guid.NewGuid().ToString(),
                Id = NewInt32(),

                AccessTokenType = AccessTokenType.Reference,
                AllowedCorsOrigins = new List<ClientCorsOrigin>(),
                AllowedCustomGrantTypes = new List<ClientCustomGrantType>(),
                AllowedScopes = new List<ClientScope>(),
                Claims = new List<ClientClaim>(),
                ClientSecrets = new List<ClientSecret>(),
                Flow = Flows.Implicit,
                Id = ,
                
            };
        }
}
