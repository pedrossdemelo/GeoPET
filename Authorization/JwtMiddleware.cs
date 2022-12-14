namespace GeoPet.Authorization;

using System.Text.Json;
using GeoPet.Interfaces;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IPetCarerService petCarerService, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var headers = context.Request.Headers;
        var headersJSON = JsonSerializer.Serialize(headers);
        Console.WriteLine(headersJSON);
        var petCarerId = jwtUtils.ValidateToken(token ?? "");
        if (petCarerId is not null)
        {
            // attach user to context on successful jwt validation
            context.Items["PetCarer"] = await petCarerService.GetCarer(petCarerId.Value);
        }

        await _next(context);
    }
}