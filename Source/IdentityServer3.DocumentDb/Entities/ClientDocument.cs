using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IdentityServer3.Core.Models;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }

        public ICollection<ClientSecret> ClientSecrets { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientName { get; set; }
        [StringLength(2000)]
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }

        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }

        public Flows Flow { get; set; }
        public bool AllowClientCredentialsOnly { get; set; }

        public ICollection<ClientRedirectUri> RedirectUris { get; set; }
        public ICollection<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        public string LogoutUri { get; set; }
        public bool LogoutSessionRequired { get; set; }

        public bool AllowAccessToAllScopes { get; set; }
        public ICollection<ClientScope> AllowedScopes { get; set; }

        // in seconds
        [Range(0, Int32.MaxValue)]
        public int IdentityTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        public int AccessTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        public int AuthorizationCodeLifetime { get; set; }

        [Range(0, Int32.MaxValue)]
        public int AbsoluteRefreshTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        public int SlidingRefreshTokenLifetime { get; set; }

        public TokenUsage RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenOnRefresh { get; set; }

        public TokenExpiration RefreshTokenExpiration { get; set; }

        public AccessTokenType AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }
        public ICollection<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        public bool IncludeJwtId { get; set; }

        public ICollection<ClientClaim> Claims { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }

        public bool AllowAccessToAllGrantTypes { get; set; }

        public ICollection<ClientCustomGrantType> AllowedCustomGrantTypes { get; set; }
        public ICollection<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
    }
}
