using System;
using IdentityServer3.DocumentDb.Repositories.Impl;
using Microsoft.Win32;

namespace IdentityServer3.DocumentDb.IntegrationTests
{
    /// <summary>
    /// Test helper that facilitates connection settings for testing
    /// </summary>
    public static class ConnectionSettingsFactory
    {
        private static readonly string s_dbid;
        private static readonly string s_endpointuri;
        private static readonly string s_authkey;

        private static string GetValue(string key, string registryPath = "HKEY_CURRENT_USER\\SOFTWARE\\IdentityServer3.DocumentDb.Tests")
        {
            string path = registryPath;
            string value = Registry.GetValue(path, key, (string) null) as string;
            if (value == null)
                throw new InvalidOperationException($"Looking for test connection setting in the registry at {path} and value {key}. Make sure to add appropriate values to registry for testing of DocDb to work");
            return value;
        }

        static ConnectionSettingsFactory()
        {
            s_dbid = GetValue("DatabaseId");
            s_endpointuri = GetValue("EndpointUri");
            s_authkey = GetValue("AuthorizationKey");
        }

        public static ConnectionSettings Create()
        {
            return new ConnectionSettings()
            {
                AuthorizationKey = s_authkey,
                DatabaseId = s_dbid,
                EndpointUri = s_endpointuri,
            };
        }
    }
}
