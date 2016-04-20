using System;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Serialization;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class AuthorizationCodeTokenDocument : TokenDocument, IEquatable<AuthorizationCodeTokenDocument>
    {
        public int CreationTimeSecondsSinceEpoch { get; set; }
        public bool IsOpenId { get; set; }
        public string Nonce { get; set; }
        public string RedirectUri { get; set; }
        public string SessionId { get; set; }
        public bool WasConsentShown { get; set; }
        public string SubjectJson { get; set; }
        public string RequestScopesJson { get; set; }

        public bool Equals(AuthorizationCodeTokenDocument other)
        {
            if (other == null) return false;
            if (!object.Equals(IsOpenId, other.IsOpenId)) return false;
            if (!object.Equals(ClientId, other.ClientId)) return false;
            if (!object.Equals(CreationTimeSecondsSinceEpoch, other.CreationTimeSecondsSinceEpoch)) return false;
            if (!object.Equals(ExpirySecondsSinceEpoch, other.ExpirySecondsSinceEpoch)) return false;
            if (!object.Equals(Id, other.Id)) return false;
            if (!object.Equals(SubjectId, other.SubjectId)) return false;
            if (!object.Equals(SubjectJson, other.SubjectJson)) return false;
            if (!object.Equals(Nonce, other.Nonce)) return false;
            if (!object.Equals(RedirectUri, other.RedirectUri)) return false;
            if (!object.Equals(SessionId, other.SessionId)) return false;
            if (!object.Equals(WasConsentShown, other.WasConsentShown)) return false;
            if (!object.Equals(RequestScopesJson, other.RequestScopesJson)) return false;
            return true;
        }

        public override string DocType { get { return DocumentTypeNames.AuthorizationCode; } }
    }
}