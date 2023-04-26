using Prokompetence.DAL.Abstractions;

namespace Prokompetence.DAL.Entities;

public sealed class UserRole : Entity<int>
{
    public string Name { get; set; }

    public ISet<User> Users { get; set; }
}