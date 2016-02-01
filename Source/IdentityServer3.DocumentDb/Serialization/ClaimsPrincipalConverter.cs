using System;
using System.Linq;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.DocumentDb.Entities;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Serialization
{
    public class ClaimsPrincipalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ClaimsPrincipal) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ClaimsPrincipalLite>(reader);
            if (source == null) return null;
            
            var claims = source.Claims.Select(x => new Claim(x.Type, x.Value));
            var id = new ClaimsIdentity(claims, source.AuthenticationType, Constants.ClaimTypes.Name, Constants.ClaimTypes.Role);
            var target = new ClaimsPrincipal(id);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (ClaimsPrincipal)value;

            var target = new ClaimsPrincipalLite
            {
                AuthenticationType = source.Identity.AuthenticationType,
                Claims = source.Claims.Select(x => new ClaimLite(x)).ToArray()
            };
            serializer.Serialize(writer, target);
        }
    }
}
