namespace Prokompetence.Model.PublicApi.Models.Users;

public sealed class UserIdentityModel
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string[] Roles { get; set; }
}