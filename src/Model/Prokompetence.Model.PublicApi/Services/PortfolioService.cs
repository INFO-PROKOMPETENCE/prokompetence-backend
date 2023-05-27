using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.Model.PublicApi.Models.Portfolio;

namespace Prokompetence.Model.PublicApi.Services;

public interface IPortfolioService
{
    Task<PortfolioProjectModel[]> GetPortfolioProjects(Guid userId, CancellationToken cancellationToken);
}

public sealed class PortfolioService : IPortfolioService
{
    private readonly IProkompetenceDbContext dbContext;

    public PortfolioService(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<PortfolioProjectModel[]> GetPortfolioProjects(Guid userId, CancellationToken cancellationToken)
    {
        var query =
            from project in dbContext.Projects
            join rating in dbContext.StudentRatingsInProject on project.Id equals rating.ProjectId
            where rating.StudentId == userId
            select new { Header = project, rating.Rating, rating.Comment };

        return await query.ProjectToType<PortfolioProjectModel>().ToArrayAsync(cancellationToken);
    }
}