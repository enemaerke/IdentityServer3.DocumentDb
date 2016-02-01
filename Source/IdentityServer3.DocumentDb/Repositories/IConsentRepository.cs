using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories
{
    public interface IConsentRepository
    {
        Task<IEnumerable<ConsentDocument>>  GetConsentBySubject(string subject);
        Task<ConsentDocument>  GetConsentBySubjectAndClient(string subject, string client);
        Task UpsertConsent(ConsentDocument document);
    }
}