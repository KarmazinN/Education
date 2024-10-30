using Education.Application.Common.DTOs;
using Education.Application.CQRS.StudentCourses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class StudentCourseController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> EnrolStudentInCourse(EnroleDto enroleDto)
        {
            return HandleResult(await Mediator.Send(new EnrolStudentnCourseQuery(enroleDto)));
        }

        [HttpDelete]
        public async Task<IActionResult> ExpelStudentFromCourse(EnroleDto enroleDto)
        {
            return HandleResult(await Mediator.Send(new ExpelStudentFromCourseQuery(enroleDto)));
        }
    }
}
