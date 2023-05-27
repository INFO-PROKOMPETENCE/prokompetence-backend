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
}