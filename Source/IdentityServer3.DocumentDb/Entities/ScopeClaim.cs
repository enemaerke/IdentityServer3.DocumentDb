using System.ComponentModel.DataAnnotations;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ScopeClaim
    {
        [Key]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }

        [StringLength(1000)]
        public virtual string Description { get; set; }

        public virtual bool AlwaysIncludeInIdToken { get; set; }

        public virtual Scope Scope { get; set; }
    }
}
