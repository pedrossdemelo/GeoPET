using GeoPet.Models.Response;

namespace GeoPet.Interfaces;

public interface ISearchService
{
    Task<AddressResponse> GetAddress(double lat, double lon);
}

