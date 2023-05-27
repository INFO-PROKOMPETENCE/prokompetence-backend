namespace Prokompetence.DAL.Entities;

public sealed class StudentInTeam
{
    public int Id { get; set; }

    public Guid TeamId { get; set; }
    public Team Team { get; set; }

    public Guid StudentId { get; set; }
    public User Student { get; set; }

    public int? RoleId { get; set; }
    public TeamRole? Role { get; set; }

    public bool IsTeamLead { get; set; }
}