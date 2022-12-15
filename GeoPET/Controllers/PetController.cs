using Microsoft.AspNetCore.Mvc;
using GeoPet.Entities;
using GeoPet.Interfaces;
using GeoPet.Authorization;
using GeoPet.Models.Request;
using GeoPet.Helpers;

namespace GeoPet.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class PetController : Controller
{
    private readonly IPetService _petService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public PetController(IPetService petService, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _petService = petService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pet>>> GetAllPets()
    {
        var pets = await _petService.GetAllPets();
        return Ok(pets);
    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Pet>> GetPetById(int id)
    {
        var pet = await _petService.GetPetById(id);
        return Ok(pet);
    }

    [HttpPost]
    public async Task<ActionResult<List<Pet>>> AddPet(PetRegisterRequest body)
    {
        var pet = await _petService.AddPet(body);
        return CreatedAtAction(nameof(GetPetById), new { id = pet.PetId }, pet);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ActionResult<List<Pet>>> UpdatePet(int id, PetRegisterRequest body)
    {
        var pet = await _petService.UpdatePet(id, body);
        return Ok(pet);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<string>> DeletePet(int id)
    {
        var success = await _petService.DeletePet(id);
        return NoContent();
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("{id}/qrcode")]
    public async Task<ActionResult> GetQRCode(int id)
    {
        var result = await _petService.GetPetById(id);
        var qrCode = QRCodeConverter.ToQRCode(result);

        ViewBag.QrCodeImage = qrCode;
        ViewBag.PetName = result.Name;

        return View();
    }
}
