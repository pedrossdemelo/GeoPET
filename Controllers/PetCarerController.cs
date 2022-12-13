using Microsoft.AspNetCore.Mvc;
using GeoPet.Models;
using GeoPet.Interfaces;

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
        return await _petCarerService.GetAllPetCarers();
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
        var result = await _petCarerService.AddPetCarer(body);
        if (result is null) return BadRequest("N motivos");
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<List<PetCarer>>> UpdatePetCarer(int id, PetCarer body)
    {
        var result = await _petCarerService.UpdatePetCarer(id, body);
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
