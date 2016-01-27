using System.ComponentModel.DataAnnotations;
using IdentityServer3.DocumentDb.Entities;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientClaim
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Type { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Value { get; set; }
    }
}
