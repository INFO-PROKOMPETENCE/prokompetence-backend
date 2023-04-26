namespace Prokompetence.Web.PublicApi.Dto.Common;

public interface IPagingParams
{
    int? Offset { get; }
    int? RowsCount { get; }
}