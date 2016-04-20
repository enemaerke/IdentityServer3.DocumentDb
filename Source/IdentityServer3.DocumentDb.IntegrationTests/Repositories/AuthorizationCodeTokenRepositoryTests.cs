using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Tests;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class AuthorizationCodeTokenRepositoryTests : BaseTokenRepositoryTest<AuthorizationCodeTokenDocument, AuthorizationCodeRepository>
    {
        protected override AuthorizationCodeRepository CreateRepo(ICollectionNameResolver resolver, ConnectionSettings settings)
        {
            return new AuthorizationCodeRepository(resolver, settings);
        }

        protected override AuthorizationCodeTokenDocument CreateDocument(string key, string clientId = null, string subjectId = null)
        {
            return ObjectMother.CreateAuthorizationCodeTokenDocument(key, clientId, subjectId);
        }
    }
}