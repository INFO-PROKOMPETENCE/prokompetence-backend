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

    /// <summary>
    /// Создать команду
    /// </summary>
    /// <param name="body"></param>
    /// <param name="ct"></param>
    [HttpPost]
    public async Task CreateTeam([FromBody] CreateTeamDto body, CancellationToken ct)
    {
        await teamService.CreateTeam(body.Name, ct);
    }

    /// <summary>
    /// Пригласить нового человека в команду
    /// </summary>
    /// <param name="body"></param>
    /// <param name="teamId"></param>
    /// <param name="ct"></param>
    [HttpPost]
    [Route("{teamId:guid}/invite")]
    public async Task InviteToTeam([FromBody] InviteToTeamDto body, [FromRoute] Guid teamId, CancellationToken ct)
    {
        await teamService.InviteToTeam(teamId, body.UserId, ct);
    }

    /// <summary>
    /// Получить список приглашений в команды
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("invitations")]
    public async Task<TeamDto[]> GetMyInvitationsToTeams(CancellationToken ct)
    {
        var teams = await teamService.GetMyInvitationsToTeams(ct);
        return teams.Adapt<TeamDto[]>();
    }

    /// <summary>
    /// Принять приглашение в команду (заявка приходит пользователю от команды, пользователь принимает)
    /// </summary>
    /// <param name="teamId"></param>
    /// <param name="ct"></param>
    [HttpPost]
    [Route("{teamId:guid}/accept-invite")]
    public async Task AcceptInviteToTeam([FromRoute] Guid teamId, CancellationToken ct)
    {
        await teamService.AcceptInvitationToTeam(teamId, ct);
    }

    /// <summary>
    /// Получить информацию о своей команде
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
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