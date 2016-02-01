using IdentityServer3.DocumentDb.Repositories.Impl;

namespace IdentityServer3.DocumentDb
{
    public class DocumentDbServiceOptions
    {
        public string DatabaseId { get; set; }
        public string EndpointUri { get; set; }
        public string AuthorizationKey { get; set; }

        internal ConnectionSettings ToConnectionSettings()
        {
            return new ConnectionSettings()
            {
                DatabaseId = DatabaseId,
                EndpointUri = EndpointUri,
                AuthorizationKey = AuthorizationKey,
            };
        }
    }
}