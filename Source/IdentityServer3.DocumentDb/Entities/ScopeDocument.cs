using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ScopeDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public bool Enabled { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [StringLength(200)]
        public string DisplayName { get; set; }
        
        [StringLength(1000)]
        public string Description { get; set; }
        
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public int Type { get; set; }
        public ICollection<ScopeClaim> ScopeClaims { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public ICollection<ScopeSecret> ScopeSecrets { get; set; }

        [StringLength(200)]
        public string ClaimsRule { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
        public bool AllowUnrestrictedIntrospection { get; set; }

    }
}
