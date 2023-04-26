namespace Prokompetence.Model.PublicApi.Models.Common;

public sealed class ListResponseModel<TItem>
{
    public TItem[] Items { get; set; }
    public int TotalCount { get; set; }
}