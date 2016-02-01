using System;
using System.Security.Claims;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClaimLite : IEquatable<ClaimLite>
    {
        public ClaimLite()
        {
        }

        public ClaimLite(Claim claim)
        {
            Type = claim.Type;
            Value = claim.Value;
            ValueType = claim.ValueType;
            Issuer = claim.Issuer;
            OriginalIssuer = claim.OriginalIssuer;
        }

        public Claim ToClaim()
        {
            var claim = new Claim(Type, Value, ValueType, Issuer, OriginalIssuer);
            return claim;
        }

        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
        public string Issuer { get; set; }
        public string OriginalIssuer { get; set; }

        public bool Equals(ClaimLite other)
        {
            return other != null &&
                   Type == other.Type &&
                   Value == other.Value &&
                   ValueType == other.ValueType &&
                   Issuer == other.Issuer &&
                   OriginalIssuer == other.OriginalIssuer;  
        }
    }
}