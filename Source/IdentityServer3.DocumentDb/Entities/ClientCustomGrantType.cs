using System.ComponentModel.DataAnnotations;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientCustomGrantType
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(250)]
        public virtual string GrantType { get; set; }

        public virtual Client Client { get; set; }
    }
}
