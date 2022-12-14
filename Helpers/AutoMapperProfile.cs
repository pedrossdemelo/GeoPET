namespace GeoPet.Helpers;

using AutoMapper;
using GeoPet.Entities;
using GeoPet.Models.Authorization;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // PetCarer -> AuthenticateResponse
        CreateMap<PetCarer, AuthenticateResponse>();

        // RegisterRequest -> PetCarer
        CreateMap<RegisterRequest, PetCarer>();

        // UpdateRequest -> PetCarer
        CreateMap<UpdateRequest, PetCarer>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}