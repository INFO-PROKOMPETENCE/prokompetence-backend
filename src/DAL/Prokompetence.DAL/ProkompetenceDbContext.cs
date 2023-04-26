using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL;

public interface IProkompetenceDbContext : IDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
}

public abstract class ProkompetenceDbContext : DbContext, IProkompetenceDbContext
{
    protected ProkompetenceDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }


    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
}