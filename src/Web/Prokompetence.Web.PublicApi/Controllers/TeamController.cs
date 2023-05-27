using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Team;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/teams")]
public sealed class TeamController : ControllerBase
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

    [HttpGet]
    [Route("invitations")]
    public async Task<TeamDto[]> GetMyInvitationsToTeams(CancellationToken ct)
    {
        var teams = await teamService.GetMyInvitationsToTeams(ct);
        return teams.Adapt<TeamDto[]>();
    }

    [HttpPost]
    [Route("{teamId:guid}/accept-invite")]
    public async Task AcceptInviteToTeam([FromRoute] Guid teamId, CancellationToken ct)
    {
        await teamService.AcceptInvitationToTeam(teamId, ct);
    }

    [HttpGet]
    [Route("my")]
    public async Task<ActionResult<TeamDto>> GetMyTeam(CancellationToken ct)
    {
        var result = await teamService.FindMyTeam(ct);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result.Adapt<TeamDto>());
    }
}