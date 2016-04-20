using IdentityServer3.DocumentDb.Repositories.Impl;

namespace IdentityServer3.DocumentDb
{
    /// <summary>
    /// Options for configuring the DocumentDb connection
    /// </summary>
    public class DocumentDbServiceOptions
    {
        /// <summary>
        /// The database id for the collections
        /// </summary>
        public string DatabaseId { get; set; }
        /// <summary>
        /// The endpoint uri for the database
        /// </summary>
        public string EndpointUri { get; set; }
        /// <summary>
        /// The authorization key for connecting to the endpoint
        /// </summary>
        public string AuthorizationKey { get; set; }

        public ICollectionNameResolver CollectionNameResolver { get; set; } = new SharedCollectionNameResolver("idsrvdocs");

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