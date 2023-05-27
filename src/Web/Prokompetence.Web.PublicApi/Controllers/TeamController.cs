using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Team;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/teams")]
public sealed class TeamController
{
    private readonly ITeamService teamService;

    public TeamController(ITeamService teamService)
    {
        this.teamService = teamService;
    }

    [HttpPost]
    public async Task CreateTeam([FromBody] CreateTeamDto body, CancellationToken ct)
    {
        await teamService.CreateTeam(body.Name, ct);
    }

    [HttpPost]
    [Route("{teamId:guid}/invite")]
    public async Task InviteToTeam([FromBody] InviteToTeamDto body, [FromRoute] Guid teamId, CancellationToken ct)
    {
        await teamService.InviteToTeam(teamId, body.UserId, ct);
    }
}