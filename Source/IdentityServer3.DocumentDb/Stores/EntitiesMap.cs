using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using AutoMapper;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Entities;
using ScopeClaim = IdentityServer3.DocumentDb.Entities.ScopeClaim;

namespace IdentityServer3.DocumentDb.Stores
{
    public static class EntitiesMap
    {
        public static IMapper Mapper
        {
            get;
            set;
        }

        static EntitiesMap()
        {
            EntitiesMap.Mapper = new MapperConfiguration((config) =>
            {
                //scope
                config.CreateMap<ScopeDocument, Scope>(MemberList.Destination)
                    .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.ScopeClaims.Select(x => x)))
                    .ForMember(x => x.ScopeSecrets, opts => opts.MapFrom(src => src.ScopeSecrets.Select(x => x)))
                    .ReverseMap()
                    .ForMember(x => x.ScopeClaims, opts => opts.MapFrom(src => src.Claims.Select(x => x)))
                    .ForMember(x => x.ScopeSecrets, opts => opts.MapFrom(src => src.ScopeSecrets.Select(x => x)));
                config.CreateMap<ScopeClaim, Core.Models.ScopeClaim>(MemberList.Destination)
                    .ReverseMap();
                config.CreateMap<ScopeSecret, Secret>(MemberList.Destination)
                    .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs.Value != null))
                    .ReverseMap();

                //client
                config.CreateMap<ClientSecret, Secret>(MemberList.Destination)
                    .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs.Value != null))
                    .ReverseMap();
                config.CreateMap<ClaimLite, Claim>(MemberList.Destination)
                    .ConstructUsing(c => c.ToClaim())
                    .ReverseMap()
                    .ConstructUsing(c => new ClaimLite(c));
                config.CreateMap<ClientDocument, Client>(MemberList.Destination)
                    .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => x.ToClaim())))
                    .ReverseMap();

                //consent
                config.CreateMap<ConsentDocument, Consent>(MemberList.Destination)
                    .ForMember(x => x.Scopes, opt => opt.MapFrom(src => src.Scopes))
                    .ReverseMap();
            }).CreateMapper();
        }

        public static Scope ToModel(this ScopeDocument s)
        {
            if (s == null) return null;
            return Mapper.Map<ScopeDocument, Scope>(s);
        }

        public static ScopeDocument ToDocument(this Scope s)
        {
            if (s == null) return null;
            return Mapper.Map<Scope, ScopeDocument>(s);
        }

        public static Client ToModel(this ClientDocument s)
        {
            if (s == null) return null;
            return Mapper.Map<ClientDocument, Client>(s);
        }

        public static ClientDocument ToDocument(this Client s)
        {
            if (s == null) return null;
            return Mapper.Map<Client, ClientDocument>(s);
        }

        public static Consent ToModel(this ConsentDocument s)
        {
            if (s == null) return null;
            return Mapper.Map<ConsentDocument, Consent>(s);
        }

        public static ConsentDocument ToDocument(this Consent s)
        {
            if (s == null) return null;
            return Mapper.Map<Consent, ConsentDocument>(s);
        }
    }
}