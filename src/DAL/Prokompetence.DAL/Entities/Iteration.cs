namespace Prokompetence.DAL.Entities;

public sealed class Iteration
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Description { get; set; }
}