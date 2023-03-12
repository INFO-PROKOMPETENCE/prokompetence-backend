using Prokompetence.DAL.Abstractions;

namespace Prokompetence.DAL.Entities;

public sealed class User : Entity<Guid>
{
    public string Name { get; set; }
}