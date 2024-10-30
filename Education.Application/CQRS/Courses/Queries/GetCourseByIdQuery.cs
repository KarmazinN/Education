using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Courses.Queries
{
    public record GetCourseByIdQuery(int Id) : IRequest<Result<CourseDto>>;
}
