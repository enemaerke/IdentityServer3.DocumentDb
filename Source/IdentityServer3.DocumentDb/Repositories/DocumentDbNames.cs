using System;
using System.Linq;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class DocumentDbNames
    {
        public const string ConsentCollectionName = "consent";
        public const string ClientCollectionName = "client";
        public const string ScopeCollectionName = "scope";
        public const string RefreshTokenCollectionName = "refreshtoken";
        public const string TokenHandleCollectionName = "tokenhandle";
        public const string AuthorizationTokenCollectionName = "tokenhandle";
    }
}