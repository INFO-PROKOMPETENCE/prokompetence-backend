namespace Prokompetence.Model.PublicApi.Models.Customer;

public class RateTeamRequest
{
    public Guid ProjectId { get; set; }
    public Guid TeamId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
}