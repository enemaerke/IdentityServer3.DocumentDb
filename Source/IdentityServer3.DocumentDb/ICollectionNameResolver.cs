namespace IdentityServer3.DocumentDb
{
    public interface ICollectionNameResolver
    {
        string ClientCollectionName { get; }
        string ScopeCollectionName { get; }
        string ConsentCollectionName { get; }
        string RefreshTokenCollectionName { get; }
        string AuthorizationCodeCollectionName { get; }
        string TokenHandleCollectionName { get; }
    }
}