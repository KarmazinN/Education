using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Courses.Queries
{
    public record DeleteCourseQuery(int id) : IRequest<Result<CourseDto>>;
}
