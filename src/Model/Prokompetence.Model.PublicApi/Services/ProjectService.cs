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
            // TODO: сделать проверку, что проект не занят другими командами
            query = query;
        }

        if (!string.IsNullOrWhiteSpace(queryParams.NameStarting))
        {
            query = query.Where(p => p.Name.StartsWith(queryParams.NameStarting));
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
}