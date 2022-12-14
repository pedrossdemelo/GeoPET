using Microsoft.AspNetCore.Mvc;
using GeoPet.Entities;
using GeoPet.Interfaces;
using System.ComponentModel.DataAnnotations;

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
    public async Task<ActionResult<PetCarer>> AddPetCarer(PetCarer body)
    {
        PetCarer? result;
        try {
            result = await _petCarerService.AddPetCarer(body);
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        }
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<List<PetCarer>>> UpdatePetCarer(int id, PetCarer body)
    {
        PetCarer? result;
        try {
            result = await _petCarerService.UpdatePetCarer(id, body);
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        }
        if (result is null) return NotFound("Pet Carer not found.");
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<string>> DeletePetCarer(int id)
    {
        var sucess = await _petCarerService.DeletePetCarer(id);
        if (!sucess) return NotFound("Pet Carer not found.");
        return NoContent();
    }
}
