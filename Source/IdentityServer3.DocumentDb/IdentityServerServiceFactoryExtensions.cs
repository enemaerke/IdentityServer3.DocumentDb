using System;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Stores;

namespace IdentityServer3.DocumentDb
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static void RegisterOperationalServices(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var connectionSettings = options.ToConnectionSettings();
            factory.Register(new Registration<ConnectionSettings>(resolver => connectionSettings));
            factory.AuthorizationCodeStore = new Registration<IAuthorizationCodeStore, AuthorizationCodeStore>();
            factory.TokenHandleStore = new Registration<ITokenHandleStore, TokenHandleStore>();
            factory.ConsentStore = new Registration<IConsentStore, ConsentStore>();
            factory.RefreshTokenStore = new Registration<IRefreshTokenStore, RefreshTokenStore>();
        }

        public static void RegisterConfigurationServices(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            factory.RegisterClientStore(options);
            factory.RegisterScopeStore(options);
        }

        public static void RegisterClientStore(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var connectionSettings = options.ToConnectionSettings();
            factory.Register(new Registration<ConnectionSettings>(resolver => connectionSettings));
            factory.ClientStore = new Registration<IClientStore, ClientStore>();
            //factory.CorsPolicyService = new ClientConfigurationCorsPolicyRegistration(options);
        }

        public static void RegisterScopeStore(this IdentityServerServiceFactory factory, DocumentDbServiceOptions options)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var connectionSettings = options.ToConnectionSettings();
            factory.Register(new Registration<ConnectionSettings>(resolver => connectionSettings));
            factory.ScopeStore = new Registration<IScopeStore, ScopeStore>();
        }
    }
}
