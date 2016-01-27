using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class TokenDocument
    {
        [JsonProperty("id")]
        public string Id { get { return TokenType + "_" + Key; } }

        public string Key { get; set; }

        public TokenType TokenType { get; set; }

        [StringLength(200)]
        public string SubjectId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string JsonCode { get; set; }

        [Required]
        public DateTimeOffset Expiry { get; set; }
    }
}
