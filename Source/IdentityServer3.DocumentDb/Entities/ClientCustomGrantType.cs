using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientCustomGrantType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string GrantType { get; set; }
    }
}
