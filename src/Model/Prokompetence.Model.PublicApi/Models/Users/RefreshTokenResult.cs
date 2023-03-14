namespace Prokompetence.Model.PublicApi.Models.Users;

public class RefreshTokenResult
{
    public bool Success { get; set; }
    public AccessTokenResult? Result { get; set; }
}