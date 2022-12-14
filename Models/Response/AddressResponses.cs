using Newtonsoft.Json;

namespace GeoPet.Models.Responses;

public class Address
{
    public string road { get; set; } = default!;
    public string suburb { get; set; } = default!;
    public string city_district { get; set; } = default!;
    public string city { get; set; } = default!;
    public string municipality { get; set; } = default!;
    public string county { get; set; } = default!;
    public string state_district { get; set; } = default!;
    public string state { get; set; } = default!;

    [JsonProperty("ISO3166-2-lvl4")]
    public string ISO31662lvl4 { get; set; } = default!;
    public string region { get; set; } = default!;
    public string postcode { get; set; } = default!;
    public string country { get; set; } = default!;
    public string country_code { get; set; } = default!;
}

public class AddressResponse
{
    public int place_id { get; set; }
    public string licence { get; set; } = default!;
    public string osm_type { get; set; } = default!;
    public int osm_id { get; set; }
    public string lat { get; set; } = default!;
    public string lon { get; set; } = default!;
    public int place_rank { get; set; }
    public string category { get; set; } = default!;
    public string type { get; set; } = default!;
    public double importance { get; set; }
    public string addresstype { get; set; } = default!;
    public string name { get; set; } = default!;
    public string display_name { get; set; } = default!;
    public Address address { get; set; } = default!;
    public List<string> boundingbox { get; set; } = default!;
}

