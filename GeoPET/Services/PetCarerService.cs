using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Entities;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using GeoPet.Helpers;
using GeoPet.Models.Authorization;
using AutoMapper;

namespace GeoPet.Services;

public class PetCarerService : IPetCarerService
{
    private async Task<bool> _validateZipCode(string zipCode)
    {
        var client = new RestClient("https://viacep.com.br/ws/");
        var request = new RestRequest(zipCode + "/json", Method.Get);
        RestResponse? response = null;
        try {
            response = await client.GetAsync(request);
        } catch {
            return false;
        }
        if (response?.Content == null || response.Content.Contains("erro")) return false;
        return true;
    }

    public async Task<PetCarer?> GetCarer(int id) {
        return await _context.PetCarers.Include(petCarer => petCarer.Pets).FirstOrDefaultAsync(petCarer => petCarer.PetCarerId == id);
    }
    private readonly GeoPetContext _context;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public PetCarerService(GeoPetContext context, IJwtUtils jwtUtils, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PetCarer> AddPetCarer(RegisterRequest body)
    {
        if (!await _validateZipCode(body.ZipCode)) throw new InvalidException("Invalid ZipCode");

        var petCarer = _mapper.Map<PetCarer>(body);

        petCarer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(body.Password);

        await _context.PetCarers.AddAsync(petCarer);

        await _context.SaveChangesAsync();

        petCarer.Pets = await _context.Pets.Where(pet => pet.PetCarerId == petCarer.PetCarerId).ToListAsync();

        return petCarer;
    }

    public async Task<bool> DeletePetCarer(int id)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;

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

    public async Task<PetCarer> UpdatePetCarer(int id, UpdateRequest body)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;

        if (petCarer is null) throw new KeyNotFoundException("PetCarer not found");
        if (body.Name is not null) petCarer.Name = body.Name;
        if (body.Email is not null) petCarer.Email = body.Email;
        if (body.ZipCode is not null)
        {
            if (!await _validateZipCode(body.ZipCode)) throw new InvalidException("Invalid ZipCode");
            petCarer.ZipCode = body.ZipCode;
        }
        if (body.Password is not null) petCarer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(body.Password);

        petCarer.Pets = await _context.Pets.Where(pet => pet.PetCarerId == petCarer.PetCarerId).ToListAsync();

        await _context.SaveChangesAsync();

        return petCarer;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest body)
    {
        var petCarer = await _context.PetCarers.FirstOrDefaultAsync(petCarer => petCarer.Email == body.Email);

        if (petCarer is null || !BCrypt.Net.BCrypt.Verify(body.Password, petCarer.PasswordHash)) throw new InvalidException("Wrong Email or Password");

        var token = _jwtUtils.GenerateToken(petCarer);

        var response = new AuthenticateResponse { Token = token, Id = petCarer.PetCarerId };

        return response;
    }

    public async Task<List<Pet>> GetPetsByCarerId(int id)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;
        var pets = await _context.Pets.Where(pet => pet.PetCarerId == petCarer.PetCarerId).ToListAsync();
        return pets;
    }
}

