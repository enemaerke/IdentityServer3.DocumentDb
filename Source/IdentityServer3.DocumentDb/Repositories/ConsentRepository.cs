using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Repositories
{
    public class ConsentRepository : CollectionBase, IConsentRepository
    {
        public ConsentRepository(ConnectionSettings settings) : base(DocumentDbNames.ConsentCollectionName, settings)
        {
        }

        public async Task<ConsentDocument> AddConsent(ConsentDocument consentDocument)
        {
            return await base.Upsert(consentDocument);
        }

        public async Task<IEnumerable<ConsentDocument>> GetConsentBySubject(string subject)
        {
            return await base.QueryAsync<ConsentDocument>(x => x.Subject == subject);
        }

        public async Task<ConsentDocument> GetConsentBySubjectAndClient(string subject, string client)
        {
            var result = await base.QueryAsync<ConsentDocument>(x => x.Subject == subject && x.ClientId == client);
            return result.FirstOrDefault();
        }
    }
}