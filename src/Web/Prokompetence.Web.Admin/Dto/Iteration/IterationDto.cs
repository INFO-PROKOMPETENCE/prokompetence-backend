namespace Prokompetence.Web.Admin.Dto.Iteration;

public sealed record IterationDto(Guid Id, DateTime StartDate, DateTime EndDate, string? Description);