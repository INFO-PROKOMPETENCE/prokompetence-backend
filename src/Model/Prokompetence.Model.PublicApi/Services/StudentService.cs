using Mapster;
using Microsoft.EntityFrameworkCore;
using Prokompetence.DAL;
using Prokompetence.Model.PublicApi.Models.Common;
using Prokompetence.Model.PublicApi.Models.Student;

namespace Prokompetence.Model.PublicApi.Services;

public interface IStudentService
{
    Task<ListResponseModel<StudentModel>>
        GetStudents(GetStudentsQuery queryParams, CancellationToken cancellationToken);
}

public sealed class StudentService : IStudentService
{
    private readonly IProkompetenceDbContext dbContext;

    public StudentService(IProkompetenceDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ListResponseModel<StudentModel>> GetStudents(GetStudentsQuery queryParams,
        CancellationToken cancellationToken)
    {
        var query = dbContext.Users
            .Where(u => u.Roles.Any(r => r.Role.Id == 2));

        if (!string.IsNullOrWhiteSpace(queryParams.NameStarts))
        {
            query = query.Where(u => u.Name.StartsWith(queryParams.NameStarts));
        }

        return new ListResponseModel<StudentModel>
        {
            Items = await query
                .OrderBy(u => u.Name)
                .Skip(queryParams.Offset ?? 0)
                .Take(queryParams.RowsCount ?? 20)
                .ProjectToType<StudentModel>()
                .ToArrayAsync(cancellationToken),

            TotalCount = await query.CountAsync(cancellationToken)
        };
    }
}