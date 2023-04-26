namespace Prokompetence.DAL.Entities;

public sealed class Organization
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ISet<Project> Projects { get; set; } // для связи 1 ко многим
}