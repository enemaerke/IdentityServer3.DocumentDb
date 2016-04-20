using System;
using IdentityServer3.DocumentDb.Repositories.Impl;
using IdentityServer3.DocumentDb.Serialization;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class RefreshTokenDocument : TokenDocument, IEquatable<RefreshTokenDocument>
    {
        public string AccessTokenJson { get; set; }
        public int Version { get; set; }
        public int CreationTimeSecondsSinceEpoch { get; set; }
        public int LifeTime { get; set; }
        public string SubjectJson { get; set; }

        public bool Equals(RefreshTokenDocument other)
        {
            if (other == null) return false;
            if (!object.Equals(AccessTokenJson, other.AccessTokenJson)) return false;
            if (!object.Equals(ClientId, other.ClientId)) return false;
            if (!object.Equals(CreationTimeSecondsSinceEpoch, other.CreationTimeSecondsSinceEpoch)) return false;
            if (!object.Equals(ExpirySecondsSinceEpoch, other.ExpirySecondsSinceEpoch)) return false;
            if (!object.Equals(Id, other.Id)) return false;
            if (!object.Equals(SubjectId, other.SubjectId)) return false;
            if (!object.Equals(SubjectJson, other.SubjectJson)) return false;
            if (!object.Equals(Version, other.Version)) return false;
            return true;
        }

        public override string DocType { get { return DocumentTypeNames.RefreshToken; } }
    }
}