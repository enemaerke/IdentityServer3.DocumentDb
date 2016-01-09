using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer3.DocumentDb.Entities
{
    public class Scope
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual bool Enabled { get; set; }
        
        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }
        
        [StringLength(200)]
        public virtual string DisplayName { get; set; }
        
        [StringLength(1000)]
        public virtual string Description { get; set; }
        
        public virtual bool Required { get; set; }
        public virtual bool Emphasize { get; set; }
        public virtual int Type { get; set; }
        public virtual ICollection<ScopeClaim> ScopeClaims { get; set; }
        public virtual bool IncludeAllClaimsForUser { get; set; }
        public virtual ICollection<ScopeSecret> ScopeSecrets { get; set; }

        [StringLength(200)]
        public virtual string ClaimsRule { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }
        public virtual bool AllowUnrestrictedIntrospection { get; set; }

    }
}
