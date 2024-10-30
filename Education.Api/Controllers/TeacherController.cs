using Education.Application.Common.DTOs;
using Education.Application.CQRS.Teachers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class TeacherController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllTeachersQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetTeacherByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeacherDto createTeacherDto)
        {
            return HandleResult(await Mediator.Send(new CreateTeacherQuery(createTeacherDto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTeacherDto updateTeacherDto)
        {
            return HandleResult(await Mediator.Send(new UpdateTeacherQuery(updateTeacherDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteTeacherQuery(id)));
        }
    }
}
