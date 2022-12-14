using GeoPet.Interfaces;
using GeoPet.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers;

[Route("[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }
    [HttpGet]
    public async Task<ActionResult<Address>> GetAddress(double lat, double lon)
    {
        var result = await _searchService.GetAddress(lat, lon);

        return Ok(result.address);
    }
}
