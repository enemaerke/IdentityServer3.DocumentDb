using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer3.DocumentDb.Entities
{
    public class Token
    {
        [Key, Column(Order = 0)]
        public virtual string Key { get; set; }

        [Key, Column(Order = 1)]
        public virtual TokenType TokenType { get; set; }

        [StringLength(200)]
        public virtual string SubjectId { get; set; }
        
        [Required]
        [StringLength(200)]
        public virtual string ClientId { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public virtual string JsonCode { get; set; }

        [Required]
        public virtual DateTimeOffset Expiry { get; set; }
    }
}
