using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ClientSecret
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public DateTimeOffset? Expiration { get; set; }
    }
}
