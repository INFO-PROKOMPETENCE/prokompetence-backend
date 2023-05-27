using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.Customer;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Customer;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/customer")]
public sealed class CustomerController : ControllerBase
{
    private readonly ICustomerService customerService;

    public CustomerController(ICustomerService customerService)
    {
        this.customerService = customerService;
    }

    [HttpPost]
    [Route("projects/rate-student")]
    public async Task RateStudent([FromBody] RateStudentDto body, CancellationToken ct)
    {
        var request = body.Adapt<RateStudentRequest>();
        await customerService.RateStudent(request, ct);
    }
    
    [HttpPost]
    [Route("projects/rate-team")]
    public async Task RateTeam([FromBody] RateTeamDto body, CancellationToken ct)
    {
        var request = body.Adapt<RateTeamRequest>();
        await customerService.RateTeam(request, ct);
    }
}