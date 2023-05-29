using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Configuration;

namespace Prokompetence.DAL.SqlServer;

public sealed class SqlServerProkompetenceDbContext : ProkompetenceDbContext
{
    public SqlServerProkompetenceDbContext(ConnectionStrings connectionStrings)
        : this(Options(connectionStrings.SqlServerProkompetence))
    {
    }

    public SqlServerProkompetenceDbContext(DbContextOptions options)
        : base(options)
    {
    }

    private static DbContextOptions Options(string connectionString) =>
        new DbContextOptionsBuilder()
            .UseSqlServer(connectionString)
            .Options;
}