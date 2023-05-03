using Prokompetence.Web.PublicApi.Dto.Common;

namespace Prokompetence.Web.PublicApi.Dto.Project;

public sealed class GetProjectRequestDto : IPagingParams
{
    public int? Offset { get; set; }
    public int? RowsCount { get; set; }
    public string? NameStarting { get; set; }
    public int? LifeScenario { get; set; }
    public int? KeyTechnologyId { get; set; }
    public bool? IsOpened { get; set; }
    public bool? IsFree { get; set; }
    public int? Complexity { get; set; }
}