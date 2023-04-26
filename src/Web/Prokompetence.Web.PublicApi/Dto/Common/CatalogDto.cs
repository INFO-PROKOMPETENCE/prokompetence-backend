namespace Prokompetence.Web.PublicApi.Dto.Common;

public sealed class CatalogDto<TItem>
{
    public TItem[] Items { get; set; }
    public int TotalCount { get; set; }
}