﻿using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;

namespace Prokompetence.Model.PublicApi.Services;

public interface ITeamService
{
    Task CreateTeam(string teamName, CancellationToken cancellationToken);
    Task InviteToTeam(Guid teamId, Guid userId, CancellationToken cancellationToken);
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
}