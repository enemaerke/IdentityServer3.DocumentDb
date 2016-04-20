namespace IdentityServer3.DocumentDb
{
    /// <summary>
    /// Represents an <see cref="ICollectionNameResolver"/> that resolves to a single collection
    /// name, effectively storing everything in one collection
    /// </summary>
    public class SharedCollectionNameResolver : ICollectionNameResolver
    {
        /// <summary>
        /// The name of the collection
        /// </summary>
        public string CollectionName { get; set; }
        public SharedCollectionNameResolver(string collectionName)
        {
            CollectionName = collectionName;
        }

        public string ClientCollectionName { get { return CollectionName;} }
        public string ScopeCollectionName { get { return CollectionName;} }
        public string ConsentCollectionName { get { return CollectionName;} }
        public string RefreshTokenCollectionName { get { return CollectionName;} }
        public string AuthorizationCodeCollectionName { get { return CollectionName;} }
        public string TokenHandleCollectionName { get { return CollectionName;} }
    }
}