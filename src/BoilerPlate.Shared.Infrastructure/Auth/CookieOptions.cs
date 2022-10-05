namespace BoilerPlate.Shared.Infrastructure.Auth;

internal class CookieOptions
{
    public bool HttpOnly { get; set; }
    public bool Secure { get; set; }
    public string? SameSite { get; set; }
}