using Prokompetence.Common.Security.Abstractions;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;

namespace Prokompetence.Model.PublicApi.Services;

public interface ITeamService
{
    Task CreateTeam(string teamName, CancellationToken cancellationToken);
}

public sealed class TeamService : ITeamService
{
    private readonly IProkompetenceDbContext dbContext;
    private readonly Func<IContextUserProvider> contextUserProviderFactory;

    public TeamService(IProkompetenceDbContext dbContext, Func<IContextUserProvider> contextUserProviderFactory)
    {
        this.dbContext = dbContext;
        this.contextUserProviderFactory = contextUserProviderFactory;
    }

    public async Task CreateTeam(string teamName, CancellationToken cancellationToken)
    {
        var user = contextUserProviderFactory.Invoke().GetUser();
        var team = new Team { Name = teamName, TeamLeadId = user.Id };
        dbContext.Teams.Add(team);
        dbContext.StudentsInTeam.Add(new StudentInTeam { Team = team, StudentId = user.Id, IsTeamLead = true });
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}