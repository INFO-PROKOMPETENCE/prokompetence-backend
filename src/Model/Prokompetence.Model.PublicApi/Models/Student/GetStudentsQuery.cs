namespace Prokompetence.Model.PublicApi.Models.Student;

public class GetStudentsQuery
{
    public int? Offset { get; init; }
    public int? RowsCount { get; init; }
    public string? NameStarts { get; init; }
    public bool HasTeam { get; init; } = true;
}