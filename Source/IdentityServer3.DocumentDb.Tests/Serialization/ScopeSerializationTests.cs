using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer3.DocumentDb.Serialization;
using IdentityServer3.DocumentDb.Tests.Mocks;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.Tests.Serialization
{
    [TestFixture]
    public class ScopeSerializationTests
    {
        [Test]
        public async Task CanSerializeAndDeserializeAScope()
        {
            var s1 = ObjectMother.CreateScope("email");
            var s2 = ObjectMother.CreateScope("read");

            var scopeRepo = new MockScopeRepository();
            scopeRepo.List.Add(s1);
            scopeRepo.List.Add(s2);

            var serializer = new JsonPropertySerializer(scopeRepo, new MockClientRepository());
            var json = await serializer.Serialize(s1);
            var deserializedScope = await serializer.Deserialize<Entities.ScopeDocument>(json);

            deserializedScope.ShouldBeEquivalentTo(s1);
        }
    }
}