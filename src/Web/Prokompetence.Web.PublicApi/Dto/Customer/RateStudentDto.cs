namespace Prokompetence.Web.PublicApi.Dto.Customer;

public record RateStudentDto(Guid StudentId, Guid ProjectId, int Rating, string Comment);