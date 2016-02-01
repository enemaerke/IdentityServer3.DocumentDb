using System.Collections.Generic;
using System.Linq;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Stores;

namespace WebHost.Config
{
    class Factory
    {
        public static IdentityServerServiceFactory Configure(DocumentDbServiceOptions documentDbServiceOptions)
        {
            var efConfig = documentDbServiceOptions;

            var cleanup = new TokenCleanup(efConfig, 10);
            cleanup.Start();

            // these two calls just pre-populate the test DB from the in-memory config
            ConfigureClients(Clients.Get(), efConfig);
            ConfigureScopes(Scopes.Get(), efConfig);

            var factory = new IdentityServerServiceFactory();

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);

            factory.UseInMemoryUsers(Users.Get());

            return factory;
        }

        public static void ConfigureClients(IEnumerable<Client> clients, DocumentDbServiceOptions options)
        {
            ClientRepository clientRepository = new ClientRepository(options.ToConnectionSettings());
            var allClients = clientRepository.GetAllClients().Result;
            if (!allClients.Any())
            {
                foreach (var c in clients)
                {
                    clientRepository.AddClient(c.ToDocument()).Wait();
                }
            }
        }

        public static void ConfigureScopes(IEnumerable<Scope> scopes, DocumentDbServiceOptions options)
        {
            ScopeRepository scopeRepository = new ScopeRepository(options.ToConnectionSettings());
            var allScopes = scopeRepository.GetAllScopes().Result;
            if (!allScopes.Any())
            {
                foreach (var s in scopes)
                {
                    scopeRepository.AddScope(s.ToDocument()).Wait();
                }
            }
        }
    }
}
