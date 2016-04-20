using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ConsentDocument : DocumentBase
    {
        public override string Id { get { return Subject + "_" + ClientId;} }

        [StringLength(200)]
        public string Subject { get; set; }

        [StringLength(200)]
        public string ClientId { get; set; }

        [Required]
        public string[] Scopes { get; set; }

        public override string DocType { get { return DocumentTypeNames.Consent; } }
    }
}
