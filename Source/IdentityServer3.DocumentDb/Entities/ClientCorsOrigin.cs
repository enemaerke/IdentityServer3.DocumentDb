using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientCorsOrigin
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Origin { get; set; }
    }
}
