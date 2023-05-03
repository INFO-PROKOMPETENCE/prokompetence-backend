using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
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
}

public sealed class ProjectService : IProjectService
{
    private readonly IProkompetenceDbContext dbContext;

    public ProjectService(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
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
            .Where(p => p.Id == projectId)
            .ProjectToType<ProjectInformationModel>()
            .FirstOrDefaultAsync(cancellationToken);
    }
}