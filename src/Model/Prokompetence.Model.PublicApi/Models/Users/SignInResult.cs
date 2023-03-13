namespace Prokompetence.Model.PublicApi.Models.Users;

public sealed class SignInResult
{
    public bool Success { get; set; }
    public string? RefreshToken { get; set; }
}