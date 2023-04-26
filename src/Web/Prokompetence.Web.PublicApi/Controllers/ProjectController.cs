using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Queries;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Common;
using Prokompetence.Web.PublicApi.Dto.Project;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/projects")]
public sealed class ProjectController : ControllerBase
{
    private readonly IProjectService projectService;

    public ProjectController(IProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] GetProjectRequestDto body,
        CancellationToken cancellationToken)
    {
        var query = body.Adapt<ProjectHeadersQuery>();
        var projectHeaders = await projectService.GetProjects(query, cancellationToken);
        return Ok(new CatalogDto<ProjectHeaderDto>
        {
            Items = projectHeaders.Items.Adapt<ProjectHeaderDto[]>(),
            TotalCount = projectHeaders.TotalCount
        });
    }

    [HttpGet]
    [Route("{projectId:guid}/header")]
    public async Task<IActionResult> GetProjectHeader([FromRoute] Guid projectId, CancellationToken cancellationToken)
    {
        var header = await projectService.FindProjectHeaderById(projectId, cancellationToken);
        if (header is null)
        {
            return NotFound();
        }

        return Ok(header.Adapt<ProjectHeaderDto>());
    }

    [HttpGet]
    [Route("{projectId:guid}/information")]
    public async Task<IActionResult> GetProjectInformation([FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var project = await projectService.FindProjectInformationById(projectId, cancellationToken);
        if (project is null)
        {
            return NotFound();
        }

        return Ok(project.Adapt<ProjectInformationDto>());
    }
}