using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Entities
{
    public class ScopeClaim
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }
    }
}
