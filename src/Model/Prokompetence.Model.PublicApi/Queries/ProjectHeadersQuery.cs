namespace Prokompetence.Model.PublicApi.Queries;

public sealed class ProjectHeadersQuery
{
    public int? Offset { get; set; }
    public int? RowsCount { get; set; }
    public string? NameStarting { get; set; }
    public int? LifeScenario { get; set; }
    public int? KeyTechnologyId { get; set; }
    public bool? IsOpened { get; set; }
    public bool? IsFree { get; set; }
}