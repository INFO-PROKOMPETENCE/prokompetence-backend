namespace Prokompetence.Model.PublicApi.Models.Users;

public sealed class AccessTokenResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
}