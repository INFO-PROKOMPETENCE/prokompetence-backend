using Prokompetence.Model.PublicApi.Models.Team;

namespace Prokompetence.Model.PublicApi.Models.Project;

public sealed class ProjectInformationModel
{
    public string Description { get; set; }
    public string FinalProject { get; set; }
    public string Stack { get; set; }
    public TeamModel[] Teams { get; set; }
}