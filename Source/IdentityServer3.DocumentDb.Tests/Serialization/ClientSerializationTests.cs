using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer3.DocumentDb.Serialization;
using IdentityServer3.DocumentDb.Tests.Mocks;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.Tests.Serialization
{
    [TestFixture]
    public class ClientSerializationTests
    {
        [Test]
        public async Task CanSerializeAndDeserializeAClient()
        {
            var client = ObjectMother.CreateClient("123");

            var clientRepo = new MockClientRepository();
            clientRepo.List.Add(client);

            var serializer = new JsonPropertySerializer(new MockScopeRepository(), clientRepo);
            var json = await serializer.Serialize(client);
            var deserializedClient = await serializer.Deserialize<Entities.ClientDocument>(json);
            
            deserializedClient.ShouldBeEquivalentTo(client);
        }
    }
}