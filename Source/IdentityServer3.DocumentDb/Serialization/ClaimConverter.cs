using System;
using System.Security.Claims;
using IdentityServer3.DocumentDb.Entities;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Serialization
{
    public class ClaimConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Claim) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ClaimLite>(reader);
            return source.ToClaim();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Claim source = (Claim)value;

            var target = new ClaimLite(source);
            serializer.Serialize(writer, target);
        }
    }
}
