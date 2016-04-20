namespace IdentityServer3.DocumentDb
{
    public class SharedCollectionNameResolver : ICollectionNameResolver
    {
        private readonly string _sharedName;
        public SharedCollectionNameResolver(string sharedName)
        {
            _sharedName = sharedName;
        }

        public string ClientCollectionName { get { return _sharedName;} }
        public string ScopeCollectionName { get { return _sharedName;} }
        public string ConsentCollectionName { get { return _sharedName;} }
        public string RefreshTokenCollectionName { get { return _sharedName;} }
        public string AuthorizationCodeCollectionName { get { return _sharedName;} }
        public string TokenHandleCollectionName { get { return _sharedName;} }
    }
}