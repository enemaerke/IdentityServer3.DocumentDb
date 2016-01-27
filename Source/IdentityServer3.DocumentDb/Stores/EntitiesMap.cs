using System.Linq;
using System.Security.Claims;
using AutoMapper;
using IdentityServer3.Core.Models;
using IdentityServer3.DocumentDb.Entities;
using ScopeClaim = IdentityServer3.DocumentDb.Entities.ScopeClaim;

namespace IdentityServer3.DocumentDb.Stores
{
    public static class EntitiesMap
    {
        static EntitiesMap()
        {
            Mapper.CreateMap<ScopeDocument, Scope>(MemberList.Destination)
                .ForMember(x => x.Claims, opts => opts.MapFrom(src => src.ScopeClaims.Select(x => x)))
                .ForMember(x => x.ScopeSecrets, opts => opts.MapFrom(src => src.ScopeSecrets.Select(x => x)));
            Mapper.CreateMap<ScopeClaim, Core.Models.ScopeClaim>(MemberList.Destination);
            Mapper.CreateMap<ScopeSecret, Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => !srs.IsSourceValueNull));

            Mapper.CreateMap<ClientSecret, Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => !srs.IsSourceValueNull));
            Mapper.CreateMap<ClientDocument, Client>(MemberList.Destination)
                .ForMember(x => x.UpdateAccessTokenClaimsOnRefresh, opt => opt.MapFrom(src => src.UpdateAccessTokenOnRefresh))
                .ForMember(x => x.AllowAccessToAllCustomGrantTypes, opt => opt.MapFrom(src => src.AllowAccessToAllGrantTypes))
                .ForMember(x => x.AllowedCustomGrantTypes, opt => opt.MapFrom(src => src.AllowedCustomGrantTypes.Select(x => x.GrantType)))
                .ForMember(x => x.RedirectUris, opt => opt.MapFrom(src => src.RedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.PostLogoutRedirectUris, opt => opt.MapFrom(src => src.PostLogoutRedirectUris.Select(x => x.Uri)))
                .ForMember(x => x.IdentityProviderRestrictions, opt => opt.MapFrom(src => src.IdentityProviderRestrictions.Select(x => x.Provider)))
                .ForMember(x => x.AllowedScopes, opt => opt.MapFrom(src => src.AllowedScopes.Select(x => x.Scope)))
                .ForMember(x => x.AllowedCorsOrigins, opt => opt.MapFrom(src => src.AllowedCorsOrigins.Select(x => x.Origin)))
                .ForMember(x => x.Claims, opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))))
                ;

            Mapper.CreateMap<ConsentDocument, Consent>(MemberList.Destination)
                .ReverseMap();
        }

        public static Scope ToModel(this ScopeDocument s)
        {
            if (s == null) return null;
            return Mapper.Map<ScopeDocument, Scope>(s);
        }

        public static Client ToModel(this ClientDocument s)
        {
            if (s == null) return null;
            return Mapper.Map<ClientDocument, Client>(s);
        }

        public static Consent ToModel(this ConsentDocument s)
        {
            if (s == null) return null;
            return Mapper.Map<ConsentDocument, Consent>(s);
        }

        public static ConsentDocument ToModel(this Consent s)
        {
            if (s == null) return null;
            return Mapper.Map<Consent, ConsentDocument>(s);
        }
    }
}