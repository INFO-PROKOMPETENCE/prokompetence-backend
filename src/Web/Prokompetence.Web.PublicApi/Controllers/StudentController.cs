using Mapster;
using Microsoft.AspNetCore.Mvc;
using Prokompetence.Model.PublicApi.Models.Student;
using Prokompetence.Model.PublicApi.Services;
using Prokompetence.Web.PublicApi.Dto.Common;
using Prokompetence.Web.PublicApi.Dto.Student;

namespace Prokompetence.Web.PublicApi.Controllers;

public sealed class StudentController : ControllerBase
{
    private readonly IStudentService studentService;

    public StudentController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    [HttpGet]
    [Route("api/students")]
    public async Task<IActionResult> GetStudents([FromQuery] GetStudentsRequestBodyDto body,
        CancellationToken cancellationToken)
    {
        var query = body.Adapt<GetStudentsQuery>();
        var result = await studentService.GetStudents(query, cancellationToken);
        return Ok(new CatalogDto<StudentDto>
        {
            Items = result.Items.Adapt<StudentDto[]>(),
            TotalCount = result.TotalCount
        });
    }
}