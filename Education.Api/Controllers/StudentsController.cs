using Education.Application.Common.DTOs;
using Education.Application.CQRS.Students.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers
{
    public class StudentsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllStudentsQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetStudentByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto createStudentDto)
        {
            return HandleResult(await Mediator.Send(new CreateStudentQuery(createStudentDto)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStudentDto updateStudentDto)
        {
            return HandleResult(await Mediator.Send(new UpdateStudentQuery(updateStudentDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteStudentQuery(id)));
        }
    }
}
