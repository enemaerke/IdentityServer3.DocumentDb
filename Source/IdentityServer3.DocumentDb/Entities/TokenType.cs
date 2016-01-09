namespace IdentityServer3.DocumentDb.Entities
{
    public enum TokenType: short
    {
        AuthorizationCode = 1,
        TokenHandle = 2,
        RefreshToken = 3
    }
}
