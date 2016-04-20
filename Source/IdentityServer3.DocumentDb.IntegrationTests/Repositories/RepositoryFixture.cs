using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories.Impl;
using Microsoft.Azure.Documents;
using NUnit.Framework;

namespace IdentityServer3.DocumentDb.IntegrationTests.Repositories
{
    [SetUpFixture]
    public class RepositoryFixture
    {
        [TearDown]
        public void TearDown()
        {
            try
            {
                var settings = TestFactory.CreateConnectionSettings();
                Console.WriteLine("Delete the database on FixtureTearDown: " + settings.DatabaseId);
                var client = RepositoryHelper.CreateClient(settings);

                //delete the database to save on storage:
                Database database = client.CreateDatabaseQuery()
                    .Where(db => db.Id == settings.DatabaseId)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (database != null)
                    client.DeleteDatabaseAsync(database.SelfLink).Wait();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Could not delete the database on FictureTearDown: " +exc.Message);
            }
        }
    }
}
