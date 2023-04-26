namespace Prokompetence.Model.PublicApi.Models.Project;

public sealed class ProjectInformationModel
{
    public string Description { get; set; }
    public string FinalProject { get; set; }
    public TeamInProjectModel[] Teams { get; set; }
}