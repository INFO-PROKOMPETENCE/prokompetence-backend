namespace Prokompetence.Web.Admin.Dto.Iteration;

public record AddIterationDto(DateTime StartDate, DateTime EndDate, string? Description);