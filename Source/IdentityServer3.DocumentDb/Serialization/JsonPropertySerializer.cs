using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Repositories;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Serialization
{
    public class JsonPropertySerializer : IPropertySerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonPropertySerializer(IScopeRepository scopeStore, IClientRepository clientStore)
        {
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.Converters.Add(new ClaimConverter());
            _serializerSettings.Converters.Add(new ClaimsPrincipalConverter());
            _serializerSettings.Converters.Add(new ScopeConverter(scopeStore));
            _serializerSettings.Converters.Add(new ClientConverter(clientStore));
        }

        public Task<T> Deserialize<T>(string propertyString)
        {
            if (propertyString == null)
                return Task.FromResult(default(T));
            return Task.FromResult(JsonConvert.DeserializeObject<T>(propertyString, _serializerSettings));
        }

        public Task<string> Serialize<T>(T propertyValue)
        {
            if (propertyValue == null)
                return Task.FromResult((string)null);
            return Task.FromResult(JsonConvert.SerializeObject(propertyValue,_serializerSettings));
        }
    }
}
