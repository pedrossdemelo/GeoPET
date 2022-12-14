using Microsoft.AspNetCore.Mvc;
using GeoPet.Entities;
using GeoPet.Interfaces;
using System.ComponentModel.DataAnnotations;
using GeoPet.Models.Authorization;

namespace GeoPet.Controllers;

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
        if (result is null) return NotFound("Pet Carer not found.");
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PetCarer>> AddPetCarer(RegisterRequest body)
    {
        var result = await _petCarerService.AddPetCarer(body);
        return CreatedAtAction(nameof(GetPetCarerById), new { id = result.PetCarerId }, result);
    }

    [HttpPatch]
    public async Task<ActionResult<List<PetCarer>>> UpdatePetCarer(UpdateRequest body)
    {
        var result = await _petCarerService.UpdatePetCarer(body);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<string>> DeletePetCarer(int id)
    {
        var success = await _petCarerService.DeletePetCarer(id);
        if (!success) return NotFound("Pet Carer not found.");
        return NoContent();
    }
}
