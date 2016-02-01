using System;
using Owin;
using WebHost.Config;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Logging;
using IdentityServer3.DocumentDb;
using Microsoft.Win32;
using Serilog;

namespace WebHost
{
    internal class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Trace()
               .CreateLogger();

            appBuilder.Map("/core", core =>
            {
                var options = new IdentityServerOptions
                {
                    SiteName = "IdentityServer3 (DocumentDb)",
                    SigningCertificate = Certificate.Get(),
                    Factory = Factory.Configure(GetOptions())
                };

                core.UseIdentityServer(options);
            });
        }

        private static DocumentDbServiceOptions GetOptions()
        {
            //reading settings from the registry, mostly to avoid check-in in comprimising keys/connectionstrings
            return new DocumentDbServiceOptions()
            {
                AuthorizationKey = GetValue("AuthorizationKey"),
                DatabaseId = GetValue("DatabaseId"),
                EndpointUri = GetValue("EndpointUri"),
            };
        }

        private static string GetValue(string key, string registryPath = "HKEY_CURRENT_USER\\SOFTWARE\\IdentityServer3.DocumentDb.WebHost")
        {
            string path = registryPath;
            string value = Registry.GetValue(path, key, (string)null) as string;
            if (value == null)
                throw new InvalidOperationException($"Looking for test connection setting in the registry at {path} and value {key}. Make sure to add appropriate values to registry for testing of DocDb to work");
            return value;
        }
    }
}