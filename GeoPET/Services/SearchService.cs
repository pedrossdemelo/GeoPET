using GeoPet.Helpers;
using GeoPet.Interfaces;
using GeoPet.Models.Response;
using Newtonsoft.Json;
using System.Globalization;

namespace GeoPet.Services;

public class SearchService : ISearchService
{
    public async Task<AddressResponse> GetAddress(double lat, double lon)
    {
        if (lat < -90 || lat > 90) throw new InvalidException("Latitude must be between -90 and 90");
        if (lon < -180 || lon > 180) throw new InvalidException("Longitude must be between -180 and 180");
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
        var requestString = string.Format(CultureInfo.InvariantCulture, "/reverse?format=jsonv2&lat={0}&lon={1}", lat, lon);
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestString);
        requestMessage.Headers.Add("Accept", "application/json");
        requestMessage.Headers.Add("User-Agent", "WHATEVER VALUE");

        var result = await client.SendAsync(requestMessage);
        var stringJson = await result.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<AddressResponse>(stringJson);

        if (dto?.address is null) throw new InvalidException("Address not found");

        return dto;
    }
}

