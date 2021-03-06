﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.DocumentDb.Entities;

namespace IdentityServer3.DocumentDb.Repositories.Impl
{
    public class ConsentRepository : RepositoryBase<ConsentDocument>, IConsentRepository
    {
        public ConsentRepository(ICollectionNameResolver resolver, ConnectionSettings settings) : base(resolver.ConsentCollectionName, DocumentTypeNames.Consent, settings)
        {
        }

        public async Task<IEnumerable<ConsentDocument>> GetConsentBySubject(string subject)
        {
            return await base.QueryAsync(x => x.Subject == subject);
        }

        public async Task<ConsentDocument> GetConsentBySubjectAndClient(string subject, string client)
        {
            var result = await base.QueryAsync(x => x.Subject == subject && x.ClientId == client);
            return result.FirstOrDefault();
        }

        public async Task UpsertConsent(ConsentDocument document)
        {
            await base.Upsert(document);
        }
    }
}