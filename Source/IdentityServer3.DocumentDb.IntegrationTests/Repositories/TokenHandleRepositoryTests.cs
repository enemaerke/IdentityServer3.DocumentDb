using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Stores;
using IdentityServer3.DocumentDb.Tests;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class TokenHandleRepositoryTests : BaseTokenRepositoryTest<TokenHandleDocument, TokenHandleRepository>
    {
        protected override TokenHandleRepository CreateRepo(ICollectionNameResolver resolver, ConnectionSettings settings)
        {
            return new TokenHandleRepository(resolver, settings);
        }

        protected override TokenHandleDocument CreateDocument(string key, string clientId = null, string subjectId = null)
        {
            return ObjectMother.CreateTokenHandleDocument(key, clientId, subjectId);
        }
    }
}
