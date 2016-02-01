using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Serialization;
using IdentityServer3.DocumentDb.Tests.Mocks;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.Tests.Serialization
{
    [TestFixture]
    public class ClaimsPrincipalSerializationTests
    {
        [Test]
        public async Task CanSerializeAndDeserializeAClaimPrincipal()
        {
            var claims = new Claim[]{
                new Claim(Constants.ClaimTypes.Subject, "alice"),
                new Claim(Constants.ClaimTypes.Scope, "read"),
                new Claim(Constants.ClaimTypes.Scope, "write"),
            };
            var ci = new ClaimsIdentity(claims, Constants.AuthenticationMethods.Password);
            var cp = new ClaimsPrincipal(ci);

            var serializer = new JsonPropertySerializer(new MockScopeRepository(), new MockClientRepository());
            var json = await serializer.Serialize(cp);
            var deserializedCp = await serializer.Deserialize<ClaimsPrincipal>(json);

            Assert.AreEqual(Constants.AuthenticationMethods.Password, deserializedCp.Identity.AuthenticationType);
            Assert.AreEqual(3, deserializedCp.Claims.Count());
            Assert.True(deserializedCp.HasClaim(Constants.ClaimTypes.Subject, "alice"));
            Assert.True(deserializedCp.HasClaim(Constants.ClaimTypes.Scope, "read"));
            Assert.True(deserializedCp.HasClaim(Constants.ClaimTypes.Scope, "write"));
        }
    }
}