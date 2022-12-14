using GeoPet.Models.Responses;

namespace GeoPet.Interfaces;

public interface ISearchService
{
    Task<AddressResponse> GetAddress(double lat, double lon);
}

