namespace GeoPet.Models.Authorization;

public class AuthenticateResponse
{
    public string Token { get; set; } = default!;
    public int Id { get; set; }
}