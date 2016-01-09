using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientSecret
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(250)]
        public virtual string Value { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        [StringLength(2000)]
        public virtual string Description { get; set; }

        public virtual DateTimeOffset? Expiration { get; set; }
        
        public virtual Client Client { get; set; }
    }
}
