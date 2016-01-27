using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ScopeClaim
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool AlwaysIncludeInIdToken { get; set; }
    }
}
