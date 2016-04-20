using System.Linq;
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
            _repo = new ConsentRepository(TestFactory.SharedCollection, TestFactory.CreateConnectionSettings());
            RepoUtil.Reset(_repo);
        }

        [Test]
        public async Task CanUpsertConsent()
        {
            var consent = ObjectMother.CreateConsentDocument("clientid1", "subject1");
            await _repo.UpsertConsent(consent);
        }

        [Test]
        public async Task CanUpsertAndRecoverConsent()
        {
            await _repo.UpsertConsent(ObjectMother.CreateConsentDocument("cli2", "subj2"));
            await _repo.UpsertConsent(ObjectMother.CreateConsentDocument("cli2", "subj3"));

            var result = await _repo.GetConsentBySubject("subj2");
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task CanUpsertAndRecoverConsentByClientAnedSubject()
        {
            await _repo.UpsertConsent(ObjectMother.CreateConsentDocument("cli4", "subj4"));
            await _repo.UpsertConsent(ObjectMother.CreateConsentDocument("cli4", "subj5"));
            await _repo.UpsertConsent(ObjectMother.CreateConsentDocument("cli5", "subj4"));
            await _repo.UpsertConsent(ObjectMother.CreateConsentDocument("cli5", "subj5"));

            var result = await _repo.GetConsentBySubjectAndClient("subj4", "cli5");
            Assert.NotNull(result);
            Assert.AreEqual("cli5", result.ClientId);
            Assert.AreEqual("subj4", result.Subject);
        }
    }
}