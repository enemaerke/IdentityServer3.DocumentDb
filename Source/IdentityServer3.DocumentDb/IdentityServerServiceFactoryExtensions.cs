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
                var nameresolver = options.CollectionNameResolver;
                factory.Register(new Registration<ICollectionNameResolver>(options.CollectionNameResolver));
                factory.Register(new Registration<ConnectionSettings>(resolver => connectionSettings));
                factory.Register(new Registration<IPropertySerializer, JsonPropertySerializer>());

                //singletons to avoid excessing docdb client creations
                factory.Register(new Registration<IConsentRepository>(new ConsentRepository(nameresolver, connectionSettings)));
                factory.Register(new Registration<IAuthorizationCodeRepository>(new AuthorizationCodeRepository(nameresolver, connectionSettings)));
                factory.Register(new Registration<IRefreshTokenRepository>(new RefreshTokenRepository(nameresolver, connectionSettings)));
                factory.Register(new Registration<ITokenHandleRepository>(new TokenHandleRepository(nameresolver, connectionSettings)));
                factory.Register(new Registration<IClientRepository>(new ClientRepository(nameresolver, connectionSettings)));
                factory.Register(new Registration<IScopeRepository>(new ScopeRepository(nameresolver, connectionSettings)));
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
