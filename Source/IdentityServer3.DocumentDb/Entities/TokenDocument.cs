using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class TokenDocument
    {
        [JsonProperty("id")]
        public string Key { get; set; }

        [StringLength(200)]
        public string SubjectId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        
        [Required]
        public DateTimeOffset Expiry { get; set; }
    }

    public class TokenHandleDocument : TokenDocument
    {
        public string Audience { get; set; }
        public string ClaimsListJson { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public string Issuer { get; set; }
        public int Lifetime { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }
    }

    public class RefreshTokenDocument : TokenDocument
    {
        public string AccessTokenJson { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public int LifeTime { get; set; }
        public string SubjectJson { get; set; }
    }

    public class AuthorizationCodeTokenDocument : TokenDocument
    {
        public DateTimeOffset CreationTime { get; set; }
        public bool IsOpenId { get; set; }
        public string Nonce { get; set; }
        public string RedirectUri { get; set; }
        public string SessionId { get; set; }
        public bool WasConsentShown { get; set; }
        public string SubjectJson { get; set; }
        public string RequestScopesJson { get; set; }
    }
}
