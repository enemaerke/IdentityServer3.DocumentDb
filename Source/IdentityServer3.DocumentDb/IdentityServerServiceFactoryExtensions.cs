using System;
using System.Linq;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Repositories;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Serialization;
using IdentityServer3.DocumentDb.Services;
using IdentityServer3.DocumentDb.Stores;

namespace IdentityServer3.DocumentDb
{
    public static class IdentityServerServiceFactoryExtensions
    {
        private static void RegisterCommonServices(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory.Registrations.All(x => x.DependencyType != typeof (IPropertySerializer)))
            {
                var connectionSettings = options.ToConnectionSettings();
                factory.Register(new Registration<ConnectionSettings>(connectionSettings));
                factory.Register(new Registration<IPropertySerializer, JsonPropertySerializer>()
                {
                    Mode = RegistrationMode.Singleton,
                });

                // repository singletons
                factory.Register(new Registration<IConsentRepository>(new ConsentRepository(connectionSettings)));
                factory.Register(new Registration<IAuthorizationCodeRepository>(new AuthorizationCodeRepository(connectionSettings)));
                factory.Register(new Registration<IRefreshTokenRepository>(new RefreshTokenRepository(connectionSettings)));
                factory.Register(new Registration<ITokenHandleRepository>(new TokenHandleRepository(connectionSettings)));
                factory.Register(new Registration<IClientRepository>(new ClientRepository(connectionSettings)));
                factory.Register(new Registration<IScopeRepository>(new ScopeRepository(connectionSettings)));
            }
        }

        /// <summary>
        /// Register the operational services, namely the token and consent stores
        /// </summary>
        public static void RegisterOperationalServices(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));

            factory.RegisterCommonServices(options);

            factory.AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, AuthorizationCodeStore>();
            factory.TokenHandleStore = new Registration<ITokenHandleStore, TokenHandleStore>();
            factory.ConsentStore = new Registration<IConsentStore, ConsentStore>();
            factory.RefreshTokenStore = new Registration<IRefreshTokenStore, RefreshTokenStore>();
        }

        /// <summary>
        /// Register the configuration services, namely the client and scope stores
        /// </summary>
        public static void RegisterConfigurationServices(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            factory.RegisterClientStore(options);
            factory.RegisterScopeStore(options);
        }

        /// <summary>
        /// Register the client store and the cors policy store
        /// </summary>
        public static void RegisterClientStore(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));

            factory.RegisterCommonServices(options);

            factory.ClientStore = new Registration<IClientStore, ClientStore>();
            factory.CorsPolicyService = new Registration<ICorsPolicyService, ClientCorsPolicyService>();
        }

        /// <summary>
        /// Register the scope store
        /// </summary>
        public static void RegisterScopeStore(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));

            factory.RegisterCommonServices(options);

            factory.ScopeStore = new Registration<IScopeStore, ScopeStore>();
        }
    }
}
