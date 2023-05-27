namespace Prokompetence.DAL.Entities;

public sealed class TeamInvitation
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
}