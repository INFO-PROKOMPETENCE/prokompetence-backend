namespace Prokompetence.DAL.Entities;

public sealed class User
{
    public Guid Id { get; set; }

    public string Login { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string? RefreshToken { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }
}