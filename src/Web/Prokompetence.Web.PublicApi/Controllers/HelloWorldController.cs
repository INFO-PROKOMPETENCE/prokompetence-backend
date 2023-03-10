using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var message = await helloWorldService.GetHelloWorld();
        return Ok(message);
    }
}