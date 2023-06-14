using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL;

public interface IProkompetenceDbContext : IUnitOfWork
{
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<Team> Teams { get; set; }
    DbSet<TeamProjectRecord> TeamProjectRecords { get; set; }
    DbSet<KeyTechnology> KeyTechnologies { get; set; }
    DbSet<LifeScenario> LifeScenarios { get; set; }
    DbSet<Organization> Organizations { get; set; }
    DbSet<StudentInTeam> StudentsInTeam { get; set; }
    DbSet<TeamRole> TeamRoles { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<UserOrganizationAccess> UserOrganizationAccesses { get; set; }
    DbSet<TeamInvitation> TeamInvitations { get; set; }
    DbSet<Iteration> Iterations { get; set; }
    DbSet<ApplicationProperty> ApplicationProperties { get; set; }
    DbSet<StudentRatingInProject> StudentRatingsInProject { get; set; }
    DbSet<TeamRatingInProject> TeamRatingsInProject { get; set; }
    DbSet<GitHubIntegration> GitHubIntegrations { get; set; }
}

public abstract class ProkompetenceDbContext : DbContext, IProkompetenceDbContext
{
    protected ProkompetenceDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamProjectRecord> TeamProjectRecords { get; set; }
    public DbSet<KeyTechnology> KeyTechnologies { get; set; }
    public DbSet<LifeScenario> LifeScenarios { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<StudentInTeam> StudentsInTeam { get; set; }
    public DbSet<TeamRole> TeamRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserOrganizationAccess> UserOrganizationAccesses { get; set; }
    public DbSet<TeamInvitation> TeamInvitations { get; set; }
    public DbSet<Iteration> Iterations { get; set; }
    public DbSet<ApplicationProperty> ApplicationProperties { get; set; }
    public DbSet<StudentRatingInProject> StudentRatingsInProject { get; set; }
    public DbSet<TeamRatingInProject> TeamRatingsInProject { get; set; }
    public DbSet<GitHubIntegration> GitHubIntegrations { get; set; }


    public new async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProkompetenceDbContext).Assembly);
    }
}