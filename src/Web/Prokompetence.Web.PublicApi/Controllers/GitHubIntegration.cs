using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.GitHubIntegration;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.GitHubIntegration;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}")]
public sealed class GitHubIntegration : ControllerBase
{
    private readonly IGitHubService gitHubService;

    public GitHubIntegration(IGitHubService gitHubService)
    {
        this.gitHubService = gitHubService;
    }

    [HttpPost]
    [Route("add-integration")]
    public async Task AddGitHubIntegration(
        [FromRoute] Guid projectId,
        [FromBody] GitHubIntegrationDto dto,
        CancellationToken cancellationToken)
    {
        var model = dto.Adapt<GitHubIntegrationModel>();
        await gitHubService.AddGitHubIntegration(projectId, model, cancellationToken);
    }

    [HttpGet]
    [Route("github-integration/commits")]
    public async Task<IActionResult> GetCommits([FromRoute] Guid projectId, CancellationToken cancellationToken)
    {
        var commits = await gitHubService.GetCommits(projectId, cancellationToken);
        return Ok(commits.Adapt<CommitDto[]>());
    }
}