using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IdentityServer3.DocumentDb.Repositories;
using IdentityServer3.DocumentDb.Repositories.Impl;
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

    [CollectionName(DocumentDbNames.TokenHandleCollectionName)]
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

    [CollectionName(DocumentDbNames.RefreshTokenCollectionName)]
    public class RefreshTokenDocument : TokenDocument
    {
        public string AccessTokenJson { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public int LifeTime { get; set; }
        public string SubjectJson { get; set; }
    }

    [CollectionName(DocumentDbNames.AuthorizationTokenCollectionName)]
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

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CollectionNameAttribute : Attribute
    {
        public string Name { get; set; }

        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
