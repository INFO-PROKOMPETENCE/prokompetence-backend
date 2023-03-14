namespace Prokompetence.Model.PublicApi.Models.Users;

public class RefreshTokenResult
{
    public bool Success { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}