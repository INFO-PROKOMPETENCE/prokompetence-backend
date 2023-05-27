using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Customer;

namespace Prokompetence.Model.PublicApi.Services;

public interface ICustomerService
{
    Task RateStudent(RateStudentRequest request, CancellationToken ct);
}

public sealed class CustomerService : ICustomerService
{
    private readonly IProkompetenceDbContext dbContext;

    public CustomerService(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task RateStudent(RateStudentRequest request, CancellationToken ct)
    {
        var currentIterationId = await GetCurrentIterationId(ct);
        var ratedInThisIteration = await dbContext.StudentRatingsInProject
            .Where(r => r.IterationId == currentIterationId
                        && r.StudentId == request.StudentId
                        && r.ProjectId == request.ProjectId)
            .AnyAsync(ct);
        if (ratedInThisIteration)
        {
            throw new Exception();
        }

        var rating = new StudentRatingInProject
        {
            StudentId = request.StudentId,
            ProjectId = request.ProjectId,
            Rating = request.Rating,
            Comment = request.Comment,
            IterationId = currentIterationId
        };
        await dbContext.StudentRatingsInProject.AddAsync(rating, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    private async Task<Guid> GetCurrentIterationId(CancellationToken ct)
    {
        const string currentIterationPropertyKey = "CurrentIteration";
        var currentIterationProperty = await dbContext.ApplicationProperties
            .Where(p => p.Key == currentIterationPropertyKey)
            .SingleOrDefaultAsync(ct);

        Guid currentIterationId;
        if (currentIterationProperty is null)
        {
            var currentIterationStartTime = await dbContext.Iterations
                .MaxAsync(i => i.StartTime, ct);
            currentIterationId = await dbContext.Iterations
                .Where(i => i.StartTime == currentIterationStartTime)
                .Select(i => i.Id)
                .FirstAsync(ct);
            currentIterationProperty = new ApplicationProperty
            {
                Key = currentIterationPropertyKey,
                Value = currentIterationId.ToString()
            };
            await dbContext.ApplicationProperties.AddAsync(currentIterationProperty, ct);
        }
        else
        {
            currentIterationId = Guid.Parse(currentIterationProperty.Value);
        }

        return currentIterationId;
    }
}