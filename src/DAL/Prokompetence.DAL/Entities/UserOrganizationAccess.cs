namespace Prokompetence.DAL.Entities;

public sealed class UserOrganizationAccess
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
}