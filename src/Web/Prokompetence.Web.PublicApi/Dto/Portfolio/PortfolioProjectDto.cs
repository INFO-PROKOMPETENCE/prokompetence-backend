using Prokompetence.Web.PublicApi.Dto.Project;

namespace Prokompetence.Web.PublicApi.Dto.Portfolio;

public sealed record PortfolioProjectDto(ProjectHeaderDto Header, int Rating, string? Comment);