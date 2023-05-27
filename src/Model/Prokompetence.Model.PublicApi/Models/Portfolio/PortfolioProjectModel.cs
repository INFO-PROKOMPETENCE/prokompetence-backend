using Prokompetence.Model.PublicApi.Models.Project;

namespace Prokompetence.Model.PublicApi.Models.Portfolio;

public sealed class PortfolioProjectModel
{
    public ProjectHeaderModel Header { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}