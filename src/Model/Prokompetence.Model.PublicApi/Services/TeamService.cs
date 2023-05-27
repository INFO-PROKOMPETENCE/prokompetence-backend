using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Team;

namespace Prokompetence.Model.PublicApi.Services;

public interface ITeamService
{
    Task CreateTeam(string teamName, CancellationToken cancellationToken);
    Task InviteToTeam(Guid teamId, Guid userId, CancellationToken cancellationToken);
    Task<TeamModel[]> GetMyInvitationsToTeams(CancellationToken cancellationToken);
    Task AcceptInvitationToTeam(Guid teamId, CancellationToken cancellationToken);
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

    public async Task InviteToTeam(Guid teamId, Guid userId, CancellationToken cancellationToken)
    {
        var teamLead = contextUserProviderFactory.Invoke().GetUser();
        var team = await dbContext.Teams.Where(t => t.Id == teamId).FirstAsync(cancellationToken);
        if (team.TeamLeadId != teamLead.Id)
        {
            throw new Exception();
        }

        var hasInvitationToThisTeam = await dbContext.TeamInvitations
            .Where(i => i.UserId == userId && i.TeamId == teamId)
            .AnyAsync(cancellationToken);
        var isUserInTeam = await dbContext.StudentsInTeam
            .Where(s => s.StudentId == userId)
            .AnyAsync(cancellationToken);

        if (hasInvitationToThisTeam || isUserInTeam)
        {
            throw new Exception();
        }

        await dbContext.TeamInvitations.AddAsync(new TeamInvitation { TeamId = teamId, UserId = userId },
            cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TeamModel[]> GetMyInvitationsToTeams(CancellationToken cancellationToken)
    {
        var user = contextUserProviderFactory.Invoke().GetUser();
        var invitations = dbContext.TeamInvitations
            .Where(i => i.UserId == user.Id)
            .Select(i => i.TeamId);
        var teams = dbContext.Teams.Where(t => invitations.Contains(t.Id));

        return await teams
            .ProjectToType<TeamModel>()
            .ToArrayAsync(cancellationToken);
    }

    public async Task AcceptInvitationToTeam(Guid teamId, CancellationToken cancellationToken)
    {
        var user = contextUserProviderFactory.Invoke().GetUser();
        var userInvitations = await dbContext.TeamInvitations
            .Where(i => i.UserId == user.Id)
            .ToArrayAsync(cancellationToken);
        if (userInvitations.All(i => i.TeamId != teamId))
        {
            throw new Exception();
        }

        var studentInTeamRecord = new StudentInTeam { TeamId = teamId, StudentId = user.Id, IsTeamLead = false };
        await dbContext.StudentsInTeam.AddAsync(studentInTeamRecord, cancellationToken);
        dbContext.TeamInvitations.RemoveRange(userInvitations);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}