using System;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Repositories;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Serialization
{
    public class ClientLite
    {
        public string ClientId { get; set; }
    }

    public class ClientConverter : JsonConverter
    {
        private readonly IClientRepository _clientStore;

        public ClientConverter(IClientRepository clientStore)
        {
            if (clientStore == null) throw new ArgumentNullException(nameof(clientStore));

            _clientStore = clientStore;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Client) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = serializer.Deserialize<ClientLite>(reader);
            var result = _clientStore.GetByClientId(source.ClientId).Result;
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (Client)value;

            var target = new ClientLite
            {
                ClientId = source.ClientId
            };
            serializer.Serialize(writer, target);
        }
    }
}
