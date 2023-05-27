namespace Prokompetence.DAL.Entities;

public sealed class TeamRatingInProject
{
    public int Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid ProjectId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public Guid IterationId { get; set; }
}