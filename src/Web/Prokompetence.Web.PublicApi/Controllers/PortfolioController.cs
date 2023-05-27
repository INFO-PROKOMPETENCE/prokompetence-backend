using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Portfolio;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/portfolio")]
public sealed class PortfolioController : ControllerBase
{
    private readonly IPortfolioService portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        this.portfolioService = portfolioService;
    }

    [HttpGet]
    [Route("users/{userId:guid}/projects")]
    public async Task<PortfolioProjectDto[]> GetPortfolioProjects([FromRoute] Guid userId, CancellationToken ct)
    {
        var result = await portfolioService.GetPortfolioProjects(userId, ct);
        return result.Adapt<PortfolioProjectDto[]>();
    }
}