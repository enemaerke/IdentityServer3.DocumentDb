namespace IdentityServer3.DocumentDb.Repositories
{
    public class DocumentDbConnectionSettings
    {
        public string EndpointUrl { get; set; }
        public string AuthorizationKey { get; set; }
        public string DatabaseId { get; set; }
    }
}