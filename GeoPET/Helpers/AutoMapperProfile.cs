namespace GeoPet.Helpers;

using AutoMapper;
using GeoPet.Entities;
using GeoPet.Models.Authorization;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // PetCarer -> AuthenticateResponse
        CreateMap<PetCarer, AuthenticateResponse>();

        // RegisterRequest -> PetCarer
        CreateMap<RegisterRequest, PetCarer>();
    }
}