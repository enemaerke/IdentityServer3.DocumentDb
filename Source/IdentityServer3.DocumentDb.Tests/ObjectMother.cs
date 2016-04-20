using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Stores;

namespace IdentityServer3.DocumentDb.Tests
{

    public class ObjectMother
    {
        private static readonly Random s_random = new Random();

        private static int NewInt32()
        {
            return s_random.Next(0, int.MaxValue);
        }

        public static Client CreateClient(string clientId = null)
        {
            clientId = clientId ?? NewInt32().ToString();
            return new Client()
            {
                ClientId = clientId,

                AllowAccessToAllCustomGrantTypes = true,

                AbsoluteRefreshTokenLifetime = 1,
                AccessTokenType = AccessTokenType.Reference,
                AccessTokenLifetime = 1,
                AllowAccessToAllScopes = true,
                AllowClientCredentialsOnly = true,
                AllowedCorsOrigins = new List<string>()
                {
                    "corsorigin",
                },
                AllowedCustomGrantTypes = new List<string>()
                {
                    "customergranttype",
                },
                AllowRememberConsent = true,
                AllowedScopes = new List<string>()
                {
                    "clientscope",
                },
                AlwaysSendClientClaims = true,
                AuthorizationCodeLifetime = 1,

                Claims = new List<Claim>()
                {
                    new Claim("claimtype", "claimvalue", "valuetype", "issuer", "originalissuer"),
                },
                ClientSecrets = new List<Secret>()
                {
                    new Secret("secrectvalue", "secretdescription", DateTimeOffset.Now),
                },
                ClientName = "client name",
                ClientUri = "client uri",

                EnableLocalLogin = true,
                Enabled = true,

                Flow = Flows.Implicit,

                IdentityProviderRestrictions = new List<string>()
                {
                    "identityprovider",
                },
                IdentityTokenLifetime = 1,
                IncludeJwtId = true,

                LogoUri = "logo-uri",
                LogoutSessionRequired = true,
                LogoutUri = "logout-uri",

                PostLogoutRedirectUris = new List<string>()
                {
                    "post-logout-redirect-uri",
                },
                PrefixClientClaims = true,

                RedirectUris = new List<string>()
                {
                    "redirect-uri",
                },
                RefreshTokenExpiration = TokenExpiration.Absolute,
                RefreshTokenUsage = TokenUsage.ReUse,
                RequireConsent = true,

                SlidingRefreshTokenLifetime = 1,

                UpdateAccessTokenClaimsOnRefresh = true,
            };
        }

        public static ClientDocument CreateClientDocument(string id = null)
        {
            //relies on the entity mapping working (there are tests for that)
            return CreateClient(id).ToDocument();
        }

        public static Consent CreateConsent(string clientId = null, string subjectId = null)
        {
            return new Consent()
            {
                ClientId = clientId ?? NewInt32().ToString(),
                Scopes = new List<string>()
                {
                    "scope1"
                },
                Subject = subjectId ?? NewInt32().ToString(),
            };
        }

        public static ConsentDocument CreateConsentDocument(string clientId = null, string subjectId = null)
        {
            return CreateConsent(clientId,subjectId).ToDocument();
        }

        public static Scope CreateScope(string scopeName = null)
        {
            return new Scope()
            {
                AllowUnrestrictedIntrospection = true,
                ClaimsRule = "claimsrule",
                Type = ScopeType.Identity,
                Description = "some description",
                Enabled = true,
                DisplayName = "some displayname",
                Emphasize = true,
                IncludeAllClaimsForUser = true,
                Name = scopeName ?? "testscopename",
                Required = true,
                Claims = new List<Core.Models.ScopeClaim>()
                {
                    new Core.Models.ScopeClaim()
                    {
                        AlwaysIncludeInIdToken = true,
                        Description = "some description",
                        Name = "scopeclaimname"
                    }
                },
                ScopeSecrets = new List<Secret>()
                {
                    new Secret("secretvalue", "description", DateTimeOffset.Now),
                },
                ShowInDiscoveryDocument = true,
            };
        }

        public static ScopeDocument CreateScopeDocument(string scopeName = null)
        {
            return CreateScope(scopeName).ToDocument();
        }

        public static TokenHandleDocument CreateTokenHandleDocument(string key, string clientId = null, string subjectId = null)
        {
            return new TokenHandleDocument()
            {
                Audience = "aud",
                ClaimsListJson = "claimsjson",
                ClientId = clientId ?? NewInt32().ToString(),
                CreationTimeSecondsSinceEpoch = DateTimeOffset.Now.ToEpoch(),
                ExpirySecondsSinceEpoch = DateTimeOffset.Now.ToEpoch(),
                Issuer = "issuer",
                Id = key,
                Lifetime = 10,
                SubjectId = subjectId ?? NewInt32().ToString(),
                Type = "type",
                Version = 1
            };
        }

        public static RefreshTokenDocument CreateRefreshTokenDocument(string key, string clientId = null, string subjectId = null)
        {
            return new RefreshTokenDocument()
            {
                ClientId = clientId ?? NewInt32().ToString(),
                CreationTimeSecondsSinceEpoch = DateTimeOffset.Now.ToEpoch(),
                ExpirySecondsSinceEpoch = DateTimeOffset.Now.ToEpoch(),
                Id = key,
                SubjectId = subjectId ?? NewInt32().ToString(),
                Version = 1,

                AccessTokenJson = "accesstokenjson",
                SubjectJson = "subjectjson",
                LifeTime = 10
            };
        }

        public static AuthorizationCodeTokenDocument CreateAuthorizationCodeTokenDocument(string key, string clientId = null, string subjectId = null)
        {
            return new AuthorizationCodeTokenDocument()
            {
                ClientId = clientId ?? NewInt32().ToString(),
                CreationTimeSecondsSinceEpoch = DateTimeOffset.Now.ToEpoch(),
                ExpirySecondsSinceEpoch = DateTimeOffset.Now.ToEpoch(),
                Id = key,
                SubjectId = subjectId ?? NewInt32().ToString(),

                IsOpenId = true,
                SubjectJson = "subjectjson",
                Nonce = "nonce",
                RequestScopesJson = "requestscopesjson",
                RedirectUri = "redirecturi",
                WasConsentShown = true,
                SessionId = "sessionid",
            };
        }
    }
}