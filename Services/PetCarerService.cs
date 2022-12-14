using System.ComponentModel.DataAnnotations;
using System.Net;
using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Entities;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using GeoPet.Helpers;

namespace GeoPet.Services;

public class PetCarerService : IPetCarerService
{
    private async Task<bool> _validateZipCode(string zipCode)
    {
        var client = new RestClient("https://viacep.com.br/ws/");
        var request = new RestRequest(zipCode + "/json", Method.Get);
        try
        {
            var response = await client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK) return true;
        }
        catch
        {
            return false;
        }
        return false;
    }
    private readonly GeoPetContext _context;

    public PetCarerService(GeoPetContext context)
    {
        _context = context;
    }

    public async Task<PetCarer> AddPetCarer(PetCarer body)
    {
        if (!await _validateZipCode(body.ZipCode)) throw new AppException("Invalid ZipCode");
        return body;
    }

    public async Task<bool> DeletePetCarer(int id)
    {
        var petCarer = await _context.PetCarers.FindAsync(id);

        if (petCarer is null) throw new KeyNotFoundException("PetCarer not found");

        _context.PetCarers.Remove(petCarer);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<PetCarer>> GetAllPetCarers()
    {
        var result = await _context.PetCarers.Include(petCarer => petCarer.Pets).ToListAsync();

        return result;
    }

    public async Task<PetCarer> GetPetCarerById(int id)
    {
        var petCarer = await _context.PetCarers.Include(petCarer => petCarer.Pets).FirstOrDefaultAsync(petCarer => petCarer.PetCarerId == id);
        if (petCarer is null) throw new KeyNotFoundException("PetCarer not found");
        return petCarer;
    }

    public async Task<PetCarer> UpdatePetCarer(int id, PetCarer body)
    {
        var petCarer = await _context.PetCarers.FindAsync(id);

        if (petCarer is null) throw new KeyNotFoundException("PetCarer not found");
        if (!await _validateZipCode(body.ZipCode.ToString())) throw new AppException("Invalid ZipCode");

        petCarer.Name = body.Name;
        petCarer.Email = body.Email;
        petCarer.ZipCode = body.ZipCode;

        await _context.SaveChangesAsync();

        return petCarer;
    }
}

