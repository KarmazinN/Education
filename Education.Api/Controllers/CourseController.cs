using Education.Application.Common.DTOs;
using Education.Application.CQRS.Courses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class CourseController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllCoursesQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetCourseByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto createCourseDto)
        {
            return HandleResult(await Mediator.Send(new CreateCourseQuery(createCourseDto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCourseDto updateCourseDto)
        {
            return HandleResult(await Mediator.Send(new UpdateCourseQuery(updateCourseDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteCourseQuery(id)));
        }
    }
}
