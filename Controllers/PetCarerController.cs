using Microsoft.AspNetCore.Mvc;
using GeoPet.Entities;
using GeoPet.Interfaces;
using System.ComponentModel.DataAnnotations;
using GeoPet.Models.Authorization;
using GeoPet.Authorization;

namespace GeoPet.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class PetCarerController : ControllerBase
{
    private readonly IPetCarerService _petCarerService;
    public PetCarerController(IPetCarerService petCarerService)
    {
        _petCarerService = petCarerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PetCarer>>> GetAllPetCarers()
    {
        var petCarers = await _petCarerService.GetAllPetCarers();

        return Ok(petCarers);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<PetCarer>> GetPetCarerById(int id)
    {
        var result = await _petCarerService.GetPetCarerById(id);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<PetCarer>> AddPetCarer(RegisterRequest body)
    {
        var result = await _petCarerService.AddPetCarer(body);
        return CreatedAtAction(nameof(GetPetCarerById), new { id = result.PetCarerId }, result);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<PetCarer>> UpdatePetCarer(int id, UpdateRequest body)
    {
        var result = await _petCarerService.UpdatePetCarer(id, body);
        return CreatedAtAction(nameof(GetPetCarerById), new { id = id }, result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeletePetCarer(int id)
    {
        await _petCarerService.DeletePetCarer(id);
        return NoContent();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest body)
    {
        var response = await _petCarerService.Authenticate(body);

        return Ok(response);
    }
}
