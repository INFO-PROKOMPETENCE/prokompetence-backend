using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.HelloWorld;
using Prokompetence.Model.PublicApi.Services;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/hello-world")]
public class HelloWorldController : ControllerBase
{
    private readonly IHelloWorldService helloWorldService;

    public HelloWorldController(IHelloWorldService helloWorldService)
    {
        this.helloWorldService = helloWorldService;
    }

    /// <summary>
    /// Пример HTTP метода
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var helloWorldRequest = new HelloWorldRequest
        {
            Name = User.Claims.Single(c => c.Type == ClaimTypes.Name).Value
        };
        var message = await helloWorldService.GetHelloWorld(helloWorldRequest, cancellationToken);
        return Ok(message);
    }
}