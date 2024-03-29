﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Security.Abstractions;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Exceptions;
using Prokompetence.Model.PublicApi.Models.Common;
using Prokompetence.Model.PublicApi.Models.Project;
using Prokompetence.Model.PublicApi.Queries;

namespace Prokompetence.Model.PublicApi.Services;

public interface IProjectService
{
    Task<ListResponseModel<ProjectHeaderModel>> GetProjects(ProjectHeadersQuery queryParams,
        CancellationToken cancellationToken);

    Task<ProjectHeaderModel?> FindProjectHeaderById(Guid projectId, CancellationToken cancellationToken);
    Task<ProjectInformationModel?> FindProjectInformationById(Guid projectId, CancellationToken cancellationToken);
    Task AddProject(AddProjectRequest request, CancellationToken cancellationToken);
    Task EnrollTeamToProject(Guid projectId, CancellationToken cancellationToken);
}

public sealed class ProjectService : IProjectService
{
    private readonly IProkompetenceDbContext dbContext;
    private readonly Func<IContextUserProvider> contextUserProviderFactory;

    public ProjectService(IProkompetenceDbContext dbContext, Func<IContextUserProvider> contextUserProviderFactory)
    {
        this.dbContext = dbContext;
        this.contextUserProviderFactory = contextUserProviderFactory;
    }

    public async Task<ListResponseModel<ProjectHeaderModel>> GetProjects(ProjectHeadersQuery queryParams,
        CancellationToken cancellationToken)
    {
        var query = dbContext.Projects.AsNoTracking();
        if (queryParams.LifeScenario.HasValue)
        {
            query = query.Where(p => p.LifeScenarioId == queryParams.LifeScenario);
        }

        if (queryParams.KeyTechnologyId.HasValue)
        {
            query = query.Where(p => p.KeyTechnologyId == queryParams.KeyTechnologyId);
        }

        if (queryParams.IsOpened.HasValue)
        {
            query = query.Where(p => p.IsOpened == queryParams.IsOpened);
        }

        if (queryParams.IsFree.HasValue)
        {
            query = query.Where(p => p.Records.Count < p.MaxTeamsCount);
        }

        if (!string.IsNullOrWhiteSpace(queryParams.NameStarting))
        {
            query = query.Where(p => p.Name.StartsWith(queryParams.NameStarting));
        }

        if (queryParams.Complexity.HasValue)
        {
            query = query.Where(p => p.Complexity == queryParams.Complexity);
        }

        return new ListResponseModel<ProjectHeaderModel>
        {
            Items = await query.Skip(queryParams.Offset ?? 0)
                .Take(queryParams.RowsCount ?? 20)
                .ProjectToType<ProjectHeaderModel>()
                .ToArrayAsync(cancellationToken),

            TotalCount = await query.CountAsync(cancellationToken)
        };
    }

    public async Task<ProjectHeaderModel?> FindProjectHeaderById(Guid projectId, CancellationToken cancellationToken)
    {
        return await dbContext.Projects.AsNoTracking()
            .Where(p => p.Id == projectId)
            .ProjectToType<ProjectHeaderModel>()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ProjectInformationModel?> FindProjectInformationById(Guid projectId,
        CancellationToken cancellationToken)
    {
        return await dbContext.Projects.AsNoTracking()
            .Include(p => p.Records)
            .ThenInclude(p => p.Project)
            .Include(p => p.Curator)
            .Where(p => p.Id == projectId)
            .ProjectToType<ProjectInformationModel>()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddProject(AddProjectRequest request, CancellationToken cancellationToken)
    {
        var currentUser = contextUserProviderFactory.Invoke().GetUser();
        var organization = await dbContext.UserOrganizationAccesses
            .Where(o => o.UserId == currentUser.Id && o.OrganizationId == request.OrganizationId)
            .FirstOrDefaultAsync(cancellationToken);
        if (!currentUser.Roles.Contains("Customer") || organization is null)
        {
            throw new HasNoAccessException();
        }

        var project = request.Adapt<Project>();
        project.CuratorId = currentUser.Id;
        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task EnrollTeamToProject(Guid projectId, CancellationToken cancellationToken)
    {
        var user = contextUserProviderFactory.Invoke().GetUser();
        var studentInTeam = await dbContext.StudentsInTeam
            .Where(s => s.StudentId == user.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (studentInTeam is not { IsTeamLead: true })
        {
            throw new Exception();
        }

        var project = await dbContext.Projects
            .Where(p => p.Id == projectId)
            .FirstOrDefaultAsync(cancellationToken);
        if (project is null)
        {
            throw new Exception();
        }

        var enrolledTeamsCount = await dbContext.TeamProjectRecords
            .Where(r => r.ProjectId == projectId)
            .CountAsync(cancellationToken);
        if (project.MaxTeamsCount <= enrolledTeamsCount)
        {
            throw new Exception();
        }

        var teamId = await dbContext.Teams
            .Where(t => t.Id == studentInTeam.TeamId)
            .Select(t => t.Id)
            .FirstAsync(cancellationToken);
        dbContext.TeamProjectRecords.Add(new TeamProjectRecord { ProjectId = projectId, TeamId = teamId });
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}