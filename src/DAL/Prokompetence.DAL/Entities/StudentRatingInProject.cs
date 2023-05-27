namespace Prokompetence.DAL.Entities;

public sealed class StudentRatingInProject
{
    public int Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ProjectId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public Guid IterationId { get; set; }
}