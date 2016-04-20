namespace IdentityServer3.DocumentDb
{
    /// <summary>
    /// Represents a resolver for influencing how the internal persistence utilizes DocumentDb
    /// collections
    /// </summary>
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