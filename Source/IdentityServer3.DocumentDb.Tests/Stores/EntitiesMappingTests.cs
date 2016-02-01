using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Stores;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.Tests.Stores
{
    [TestFixture]
    public class EntitiesMappingTests
    {
        [Test]
        public void CheckClient()
        {
            var original = ObjectMother.CreateClient();
            var document = original.ToDocument();
            var restored = document.ToModel();

            restored.ShouldBeEquivalentTo(original, opt =>
            {
                opt.Excluding(c => c.Claims);
                return opt;
            });

            Assert.AreEqual(original.Claims.Count, restored.Claims.Count);

            //manually comparing claims
            foreach (var claim in original.Claims)
            {
                var claimLite = new ClaimLite(claim);
                Assert.True(restored.Claims.Exists(c => new ClaimLite(c).Equals(claimLite)), "could not match claim: " + claim.ToString());
            }
        }

        [Test]
        public void CheckConsent()
        {
            var original = ObjectMother.CreateConsent();
            var document = original.ToDocument();
            var restored = document.ToModel();

            restored.ShouldBeEquivalentTo(original);
        }

        [Test]
        public void CheckScope()
        {
            var original = ObjectMother.CreateScope();
            var document = original.ToDocument();
            var restored = document.ToModel();

            restored.ShouldBeEquivalentTo(original);
        }
    }
}
