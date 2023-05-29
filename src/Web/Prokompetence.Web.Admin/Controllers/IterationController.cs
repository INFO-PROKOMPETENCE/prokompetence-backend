using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Web.Admin.Dto.Iteration;

namespace Prokompetence.Web.Admin.Controllers;

[ApiController]
[Route("api/admin/iterations")]
public sealed class IterationController : ControllerBase
{
    private readonly IProkompetenceDbContext dbContext;

    public IterationController(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IterationDto[]> GetAllIterations()
    {
        var iterations = await dbContext.Iterations.OrderBy(i => i.StartTime).ToArrayAsync();
        return iterations.Select(i => new IterationDto(i.Id, i.StartTime, i.EndTime, i.Description)).ToArray();
    }

    [HttpPost]
    public async Task AddIteration([FromBody] AddIterationDto body)
    {
        var iterationEntity = new Iteration
        {
            StartTime = body.StartDate.ToUniversalTime(),
            EndTime = body.EndDate.ToUniversalTime(),
            Description = body.Description
        };

        await dbContext.Iterations.AddAsync(iterationEntity);
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }

    [HttpGet]
    [Route("current")]
    public async Task<IActionResult> GetCurrentIteration()
    {
        var property = await dbContext.ApplicationProperties
            .Where(p => p.Key == "CurrentIteration")
            .SingleOrDefaultAsync();
        if (!Guid.TryParse(property?.Value, out var iterationId))
        {
            return NotFound();
        }

        var iteration = await dbContext.Iterations
            .Where(i => i.Id == iterationId)
            .FirstAsync();
        return Ok(new IterationDto(iteration.Id, iteration.StartTime, iteration.EndTime, iteration.Description));
    }

    [HttpPost]
    [Route("current")]
    public async Task<IActionResult> SetCurrentIteration([FromBody] SetCurrentIterationDto body)
    {
        var iterationId = body.IterationId;
        var hasIterationWithId = await dbContext.Iterations
            .Where(i => i.Id == iterationId)
            .AnyAsync();
        if (!hasIterationWithId)
        {
            return BadRequest($"Итерации с id {iterationId} не существует");
        }

        var property = await dbContext.ApplicationProperties
            .Where(p => p.Key == "CurrentIteration")
            .SingleOrDefaultAsync();
        if (property is null)
        {
            property = new ApplicationProperty { Key = "CurrentIteration", Value = iterationId.ToString() };
            await dbContext.ApplicationProperties.AddAsync(property);
        }
        else
        {
            property.Value = iterationId.ToString();
        }

        await dbContext.SaveChangesAsync(CancellationToken.None);
        return Ok();
    }
}