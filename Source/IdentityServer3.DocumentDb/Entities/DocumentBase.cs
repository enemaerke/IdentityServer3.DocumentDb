using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public abstract class DocumentBase : IDocument
    {
        [JsonProperty("id")]
        public virtual string Id { get; set; }

        [JsonProperty("doc_type")]
        public abstract string DocType { get; }
    }
}