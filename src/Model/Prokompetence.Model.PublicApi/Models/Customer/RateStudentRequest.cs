namespace Prokompetence.Model.PublicApi.Models.Customer;

public sealed class RateStudentRequest
{
    public Guid StudentId { get; set; }
    public Guid ProjectId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}