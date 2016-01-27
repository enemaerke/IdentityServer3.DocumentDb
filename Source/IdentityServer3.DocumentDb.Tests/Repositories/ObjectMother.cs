using System;
using System.Collections.Generic;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Tests.Repositories
{

    public class ObjectMother
    {
        private static readonly Random s_random = new Random();

        private static int NewInt32()
        {
            return s_random.Next();
        }

        public static ClientDocument CreateClient(string id = null)
        {
            id = id ?? Guid.NewGuid().ToString();
            return new ClientDocument()
            {
                ClientId = id,
                Id = NewInt32().ToString(),

                AbsoluteRefreshTokenLifetime = 1,
                AccessTokenType = AccessTokenType.Reference,
                AccessTokenLifetime = 1,
                AllowAccessToAllGrantTypes = true,
                AllowAccessToAllScopes = true,
                AllowClientCredentialsOnly = true,
                AllowedCorsOrigins = new List<ClientCorsOrigin>()
                {
                    new ClientCorsOrigin()
                    {
                        Id = 1,
                        Origin = "origin",
                    },
                },
                AllowedCustomGrantTypes = new List<ClientCustomGrantType>()
                {
                    new ClientCustomGrantType()
                    {
                        GrantType = "granttype",
                        Id = 1,
                    }
                },
                AllowRememberConsent = true,
                AllowedScopes = new List<ClientScope>()
                {
                    new ClientScope()
                    {
                        Id = 1,
                        Scope = "clientscope",
                    }
                },
                AlwaysSendClientClaims = true,
                AuthorizationCodeLifetime = 1,

                Claims = new List<ClientClaim>()
                {
                    new ClientClaim()
                    {
                        Id = 1,
                        Type = "email",
                        Value = "some email",
                    }
                },
                ClientSecrets = new List<ClientSecret>()
                {
                    new ClientSecret()
                    {
                        Id = 1,
                        Description = "some description",
                        Expiration = DateTimeOffset.Now,
                        Type = "type",
                        Value = "value",
                    }
                },
                ClientName = "client name",
                ClientUri = "client uri",

                EnableLocalLogin = true,
                Enabled = true,

                Flow = Flows.Implicit,

                IdentityProviderRestrictions = new List<ClientIdPRestriction>()
                {
                    new ClientIdPRestriction()
                    {
                        Id = 1,
                        Provider = "provider",
                    }
                },
                IdentityTokenLifetime = 1,
                IncludeJwtId = true,

                LogoUri = "logo-uri",
                LogoutSessionRequired = true,
                LogoutUri = "logout-uri",

                PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>()
                {
                    new ClientPostLogoutRedirectUri()
                    {
                        Id = 1,
                        Uri = "some logout redirect uri"
                    }
                },
                PrefixClientClaims = true,

                RedirectUris = new List<ClientRedirectUri>()
                {
                    new ClientRedirectUri()
                    {
                        Id = 1,
                        Uri = "redirect uri"
                    }
                },
                RefreshTokenExpiration = TokenExpiration.Absolute,
                RefreshTokenUsage = TokenUsage.ReUse,
                RequireConsent = true,

                SlidingRefreshTokenLifetime = 1,

                UpdateAccessTokenOnRefresh = true,

            };
        }

        public static ConsentDocument CreateConsent()
        {
            return new ConsentDocument()
            {
                ClientId = "clientid",
                Subject = "subject",
                Scopes = "myscopes"
            };
        }
    }
}