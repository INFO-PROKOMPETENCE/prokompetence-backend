using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.EFCore;

public abstract class ProkompetenceDbContext : DbContext
{
    protected ProkompetenceDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> Roles { get; set; }
}