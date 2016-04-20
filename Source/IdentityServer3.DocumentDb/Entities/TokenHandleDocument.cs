using System;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Serialization;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class TokenHandleDocument : TokenDocument, IEquatable<TokenHandleDocument>
    {
        public string Audience { get; set; }
        public string ClaimsListJson { get; set; }
        public int CreationTimeSecondsSinceEpoch { get; set; }
        public string Issuer { get; set; }
        public int Lifetime { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }

        public bool Equals(TokenHandleDocument other)
        {
            if (other == null) return false;
            if (!object.Equals(Audience,other.Audience)) return false;
            if (!object.Equals(ClaimsListJson, other.ClaimsListJson)) return false;
            if (!object.Equals(ClientId, other.ClientId)) return false;
            if (!object.Equals(CreationTimeSecondsSinceEpoch, other.CreationTimeSecondsSinceEpoch)) return false;
            if (!object.Equals(ExpirySecondsSinceEpoch, other.ExpirySecondsSinceEpoch)) return false;
            if (!object.Equals(Issuer, other.Issuer)) return false;
            if (!object.Equals(Id, other.Id)) return false;
            if (!object.Equals(Lifetime, other.Lifetime)) return false;
            if (!object.Equals(SubjectId, other.SubjectId)) return false;
            if (!object.Equals(Type, other.Type)) return false;
            if (!object.Equals(Version, other.Version)) return false;
            return true;
        }

        public override string DocType { get { return DocumentTypeNames.TokenHandle; } }
    }
}