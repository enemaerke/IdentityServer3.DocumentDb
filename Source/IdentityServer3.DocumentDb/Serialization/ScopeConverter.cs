﻿using System;
using System.Linq;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Repositories;
using Newtonsoft.Json;

namespace IdentityServer3.DocumentDb.Serialization
{
    public class ScopeConverter : JsonConverter
    {
        private readonly IScopeRepository _scopeRepo;

        public ScopeConverter(IScopeRepository scopeRepo)
        {
            if (scopeRepo == null) throw new ArgumentNullException(nameof(scopeRepo));

            this._scopeRepo = scopeRepo;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Scope) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var name = serializer.Deserialize<string>(reader);
            var result = _scopeRepo.GetByScopeNames(new string[]{name}).Result;
            return result.Single();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var source = (Scope)value;
            serializer.Serialize(writer, source.Name);
        }
    }
}
