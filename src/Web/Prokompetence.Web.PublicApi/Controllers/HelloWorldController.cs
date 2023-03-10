using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.HelloWorld;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.HelloWorld;

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
    public async Task<IActionResult> Index([FromQuery] HelloWorldDto dto)
    {
        var helloWorldRequest = dto.Adapt<HelloWorldRequest>();
        var message = await helloWorldService.GetHelloWorld(helloWorldRequest);
        return Ok(message);
    }
}