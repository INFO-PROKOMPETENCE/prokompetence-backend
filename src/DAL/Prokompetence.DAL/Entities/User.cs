using Prokompetence.DAL.Abstractions;

namespace Prokompetence.DAL.Entities;

public sealed class User : Entity<Guid>
{
    public string Login { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public string? RefreshToken { get; set; }
}