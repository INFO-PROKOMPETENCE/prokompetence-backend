namespace Prokompetence.DAL.Entities;

public sealed class UserRole
{
    public int Id { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}