using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ScopeSecret
    {
        [Key]
        public virtual int Id { get; set; }

        [StringLength(1000)]
        public virtual string Description { get; set; }

        public virtual DateTimeOffset? Expiration { get; set; }

        [StringLength(250)]
        public virtual string Type { get; set; }

        [Required]
        [StringLength(250)]
        public virtual string Value { get; set; }

        public virtual Scope Scope { get; set; }
    }
}
