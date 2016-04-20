using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Tests;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [TestFixture]
    public class ClientRepositoryTests
    {
        private readonly ClientRepository _repo;

        public ClientRepositoryTests()
        {
            _repo = new ClientRepository(TestFactory.SharedCollection, TestFactory.CreateConnectionSettings());
            RepoUtil.Reset(_repo);
        }

        [Test]
        public async Task CanLookForUnknownClientWithoutExceptions()
        {
            var result = await _repo.GetByClientId(Guid.NewGuid().ToString());
            Assert.IsNull(result);
        }

        [Test]
        public async Task CanGetAll()
        {
            RepoUtil.Reset(_repo);

            await _repo.AddClient(ObjectMother.CreateClientDocument("id1"));
            await _repo.AddClient(ObjectMother.CreateClientDocument("id2"));
            await _repo.AddClient(ObjectMother.CreateClientDocument("id3"));

            var result = await _repo.GetAllClients();
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public async Task CanQueryExistingClient()
        {
            var client = ObjectMother.CreateClientDocument();
            var added = await _repo.AddClient(client);

            var returned = await _repo.GetByClientId(client.ClientId);
            Assert.NotNull(returned);

            client.ShouldBeEquivalentTo(returned, options =>
            {
                options.Excluding(x => x.Id);
                return options;
            });

            var deleteResult = await _repo.DeleteClientById(returned.Id);
            Assert.True(deleteResult);
        }
    }
}