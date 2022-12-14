using System.Diagnostics.CodeAnalysis;

namespace GeoPet.Models.Authorization;

[ExcludeFromCodeCoverage]
public class AuthenticateResponse
{
    public string Token { get; set; } = default!;
}