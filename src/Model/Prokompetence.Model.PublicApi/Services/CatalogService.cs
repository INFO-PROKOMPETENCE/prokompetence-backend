using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.Model.PublicApi.Models.Catalog;

namespace Prokompetence.Model.PublicApi.Services;

public interface ICatalogService
{
    Task<KeyTechnologyModel[]> GetKeyTechnologies(CancellationToken cancellationToken);
    Task<LifeScenarioModel[]> GetLifeScenarios(CancellationToken cancellationToken);
    Task<TeamRolesModel[]> GetTeamRoles(CancellationToken cancellationToken);
}

public sealed class CatalogService : ICatalogService
{
    private readonly IProkompetenceDbContext dbContext;

    public CatalogService(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<KeyTechnologyModel[]> GetKeyTechnologies(CancellationToken cancellationToken)
    {
        return await dbContext.KeyTechnologies.AsNoTracking()
            .ProjectToType<KeyTechnologyModel>()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<LifeScenarioModel[]> GetLifeScenarios(CancellationToken cancellationToken)
    {
        return await dbContext.LifeScenarios.AsNoTracking()
            .ProjectToType<LifeScenarioModel>()
            .ToArrayAsync(cancellationToken);
    }

    public async Task<TeamRolesModel[]> GetTeamRoles(CancellationToken cancellationToken)
    {
        return await dbContext.TeamRoles.AsNoTracking()
            .ProjectToType<TeamRolesModel>()
            .ToArrayAsync(cancellationToken);
    }
}