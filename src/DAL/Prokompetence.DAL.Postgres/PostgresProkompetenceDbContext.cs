using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Configuration;

namespace Prokompetence.DAL.Postgres;

public sealed class PostgresProkompetenceDbContext : ProkompetenceDbContext
{
    public PostgresProkompetenceDbContext(ConnectionStrings connectionStrings)
        : this(Options(connectionStrings.Prokompetence))
    {
    }

    public PostgresProkompetenceDbContext(DbContextOptions options)
        : base(options)
    {
    }

    private static DbContextOptions Options(string connectionString) =>
        new DbContextOptionsBuilder()
            .UseNpgsql(connectionString)
            .Options;
}