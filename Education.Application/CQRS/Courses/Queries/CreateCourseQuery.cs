using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Courses.Queries
{
    public record CreateCourseQuery(CreateCourseDto newCourse) : IRequest<Result<CreateCourseDto>>;
}
