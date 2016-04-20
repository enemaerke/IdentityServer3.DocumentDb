using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Client.TransientFaultHandling;
using Microsoft.Azure.Documents.Client.TransientFaultHandling.Strategies;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    internal static class RepositoryHelper
    {
        internal static IReliableReadWriteDocumentClient CreateClient(ConnectionSettings settings)
        {
            var client = new DocumentClient(new Uri(settings.EndpointUri), settings.AuthorizationKey, new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });
            var documentRetryStrategy = new DocumentDbRetryStrategy(RetryStrategy.DefaultExponential) { FastFirstRetry = true };
            return client.AsReliable(documentRetryStrategy);
        }
    }
}