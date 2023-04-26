namespace Prokompetence.DAL.Entities;

public sealed class TeamProjectRecord
{
    public int Id { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public Guid TeamId { get; set; }
    public Team Team { get; set; }
}