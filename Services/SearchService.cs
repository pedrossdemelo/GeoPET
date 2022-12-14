using GeoPet.Helpers;
using GeoPet.Interfaces;
using GeoPet.Models.Responses;
using Newtonsoft.Json;
using System.Globalization;

namespace GeoPet.Services;

public class SearchService : ISearchService
{
    public async Task<AddressResponse> GetAddress(double lat, double lon) 
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, string.Format(CultureInfo.InvariantCulture, "/reverse?format=jsonv2&lat={0}&lon={1}", lat, lon));
        requestMessage.Headers.Add("Accept", "application/json");
        requestMessage.Headers.Add("User-Agent", "WHATEVER VALUE");

        var result = await client.SendAsync(requestMessage);
        var stringJson = await result.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<AddressResponse>(stringJson);

        if (dto is null) throw new AppException("Address not found");

        return dto;
    }
}

