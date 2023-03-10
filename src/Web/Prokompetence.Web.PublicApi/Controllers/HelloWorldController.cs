using Microsoft.AspNetCore.Mvc;

namespace Prokompetence.Web.PublicApi.Controllers;

[ApiController]
[Route("api/hello-world")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(Ok("Hello world!"));
    }
}