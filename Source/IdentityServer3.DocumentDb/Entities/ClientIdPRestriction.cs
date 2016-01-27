using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientIdPRestriction
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Provider { get; set; }
    }
}
