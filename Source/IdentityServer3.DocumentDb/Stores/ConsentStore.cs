using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.DocumentDb.Entities;
using IdentityServer3.DocumentDb.Interfaces;

namespace IdentityServer3.DocumentDb.Stores
{
    public class ConsentStore : IConsentStore
    {
        private readonly IConsentRepository _repository;

        public ConsentStore(IConsentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Consent>> LoadAllAsync(string subject)
        {
            var consentDoc = await _repository.GetConsentBySubject(subject);
            return consentDoc.Select(x => EntitiesMap.ToModel((ConsentDocument) x));
        }

        public Task RevokeAsync(string subject, string client)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Consent> LoadAsync(string subject, string client)
        {
            var consentDoc = await _repository.GetConsentBySubjectAndClient(subject, client);
            return consentDoc.ToModel();
        }

        public Task UpdateAsync(Consent consent)
        {
            throw new System.NotImplementedException();
        }
    }
}