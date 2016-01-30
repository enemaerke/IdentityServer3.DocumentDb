using System;
using System.Linq;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Repositories;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Serialization
{
    public class ScopeLite
    {
        public string Name { get; set; }
    }

    public class ScopeConverter : JsonConverter
    {
        private readonly IScopeRepository scopeStore;

        public ScopeConverter(IScopeRepository scopeStore)
        {
            if (scopeStore == null) throw new ArgumentNullException(nameof(scopeStore));

            this.scopeStore = scopeStore;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Scope) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ScopeLite>(reader);
            var result = scopeStore.GetByScopeNames(new string[]{source.Name}).Result;
            return result.Single();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (Scope)value;

            var target = new ScopeLite
            {
                Name = source.Name
            };
            serializer.Serialize(writer, target);
        }
    }
}
