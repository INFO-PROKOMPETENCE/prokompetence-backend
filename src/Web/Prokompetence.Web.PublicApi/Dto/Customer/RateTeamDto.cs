namespace Prokompetence.Web.PublicApi.Dto.Customer;

public sealed record RateTeamDto(Guid TeamId, Guid ProjectId, int Rating, string? Comment);