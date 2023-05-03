using System.Net;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.Organization;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Organization;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/organizations")]
public sealed class OrganizationController : ControllerBase
{
    private readonly IOrganizationService organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        this.organizationService = organizationService;
    }

    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> AddOrganization([FromBody] AddOrganizationBodyDto body, CancellationToken ct)
    {
        var requestModel = body.Adapt<AddOrganizationRequest>();
        await organizationService.AddOrganization(requestModel, ct);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet]
    [Route("my")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetMyOrganizations(CancellationToken ct)
    {
        var result = await organizationService.GetCurrentUsersOrganizations(ct);
        return Ok(result.Adapt<OrganizationDto[]>());
    }
}