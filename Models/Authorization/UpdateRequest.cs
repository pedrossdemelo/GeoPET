namespace GeoPet.Models.Authorization;

public class UpdateRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}