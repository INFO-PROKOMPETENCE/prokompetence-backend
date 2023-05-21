using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Prokompetence.DAL.Postgres;

public sealed class PostgresDatabaseInstanceFactory : IDesignTimeDbContextFactory<PostgresProkompetenceDbContext>
{
    public PostgresProkompetenceDbContext CreateDbContext(string[] args) =>
        new(new DbContextOptionsBuilder<PostgresProkompetenceDbContext>().UseNpgsql().Options);
}