using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Tests;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class RefreshTokenRepositoryTests : BaseTokenRepositoryTest<RefreshTokenDocument, RefreshTokenRepository>
    {
        protected override RefreshTokenRepository CreateRepo(ICollectionNameResolver resolver, ConnectionSettings settings)
        {
            return new RefreshTokenRepository(resolver, settings);
        }

        protected override RefreshTokenDocument CreateDocument(string key, string clientId = null, string subjectId = null)
        {
            return ObjectMother.CreateRefreshTokenDocument(key, clientId, subjectId);
        }
    }
}