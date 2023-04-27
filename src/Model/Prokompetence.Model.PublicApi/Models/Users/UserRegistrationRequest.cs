namespace Prokompetence.Model.PublicApi.Models.Users;

public sealed class UserRegistrationRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}