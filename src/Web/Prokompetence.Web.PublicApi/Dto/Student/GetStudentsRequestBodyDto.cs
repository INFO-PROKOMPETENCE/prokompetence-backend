using Prokompetence.Web.PublicApi.Dto.Common;

namespace Prokompetence.Web.PublicApi.Dto.Student;

public sealed record GetStudentsRequestBodyDto : IPagingParams
{
    public int? Offset { get; init; }
    public int? RowsCount { get; init; }
    public string? NameStarts { get; init; }
}