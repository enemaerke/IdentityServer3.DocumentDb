using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public abstract class TokenDocument : DocumentBase
    {
        [StringLength(200)]
        public string SubjectId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        
        [Required]
        public int ExpirySecondsSinceEpoch { get; set; }

        [JsonIgnore]
        public DateTimeOffset Expiry
        {
            get { return ExpirySecondsSinceEpoch.FromEpoch(); }
            set { ExpirySecondsSinceEpoch = value.ToEpoch(); }
        }
    }
}
