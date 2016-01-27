using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientScope
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Scope { get; set; }
    }
}
