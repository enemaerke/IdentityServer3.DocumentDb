using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Repositories.Impl;

namespace IdentityServer3.DocumentDb
{
    public class IndividualCollectionNameResolver : ICollectionNameResolver
    {
        public string ClientCollectionName { get; set; } = DocumentTypeNames.Client;
        public string ScopeCollectionName { get; set; } = DocumentTypeNames.Scope;
        public string ConsentCollectionName { get; set; } = DocumentTypeNames.Consent;
        public string RefreshTokenCollectionName { get; set; } = DocumentTypeNames.RefreshToken;
        public string AuthorizationCodeCollectionName { get; set; } = DocumentTypeNames.AuthorizationCode;
        public string TokenHandleCollectionName { get; set; } = DocumentTypeNames.TokenHandle;
    }
}