using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Catalog;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/catalogs")]
public sealed class CatalogController : ControllerBase
{
    private readonly ICatalogService catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    [HttpGet]
    [Route("key-technologies")]
    public async Task<IActionResult> GetKeyTechnologies(CancellationToken cancellationToken)
    {
        var model = await catalogService.GetKeyTechnologies(cancellationToken);
        return Ok(model.Adapt<KeyTechnologyDto[]>());
    }

    [HttpGet]
    [Route("life-scenarios")]
    public async Task<IActionResult> GetLifeScenarios(CancellationToken cancellationToken)
    {
        var model = await catalogService.GetLifeScenarios(cancellationToken);
        return Ok(model.Adapt<LifeScenarioDto[]>());
    }

    [HttpGet]
    [Route("team-roles")]
    public async Task<IActionResult> GetTeamRoles(CancellationToken cancellationToken)
    {
        var model = await catalogService.GetTeamRoles(cancellationToken);
        return Ok(model.Adapt<TeamRoleDto[]>());
    }
}