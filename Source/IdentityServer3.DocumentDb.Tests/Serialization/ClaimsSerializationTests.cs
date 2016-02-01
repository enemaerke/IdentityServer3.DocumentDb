using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.DocumentDb.Serialization;
using IdentityServer3.DocumentDb.Tests.Mocks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.Tests.Serialization
{
    [TestFixture]
    public class ClaimsSerializationTests
    {
        [Test]
        public async Task CanSerializeAndDeserializeAClaim()
        {
            var claim = new Claim(Constants.ClaimTypes.Subject, "alice");

            var serializer = new JsonPropertySerializer(new MockScopeRepository(), new MockClientRepository());
            var json = await serializer.Serialize(claim);
            var deserializedClaim = await serializer.Deserialize<Claim>(json);

            Assert.AreEqual(Constants.ClaimTypes.Subject, deserializedClaim.Type);
            Assert.AreEqual(claim.Value, deserializedClaim.Value);
        }
    }
}
