using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientRedirectUri
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(2000)]
        public virtual string Uri { get; set; }
    }
}
