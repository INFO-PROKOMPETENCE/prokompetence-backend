using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Prokompetence.DAL.SqlServer;

public class SqlServerDatabaseInstanceFactory : IDesignTimeDbContextFactory<SqlServerProkompetenceDbContext>
{
    public SqlServerProkompetenceDbContext CreateDbContext(string[] args)
        => new(new DbContextOptionsBuilder<SqlServerProkompetenceDbContext>().UseSqlServer().Options);
}