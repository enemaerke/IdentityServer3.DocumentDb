using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Interfaces
{
    public interface IClientConfigurationRepository
    {
        Task<ClientDocument> GetByClientId(string clientId);
    }

    public interface IScopeConfigurationRepository
    {
        Task<IEnumerable<ScopeDocument>> GetByScopeNames(string[] scopeNames);
    }

    public interface IConsentRepository
    {
        Task<IEnumerable<ConsentDocument>>  GetConsentBySubject(string subject);
        Task<ConsentDocument>  GetConsentBySubjectAndClient(string subject, string client);
    }
}
