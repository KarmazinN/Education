using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.StudentCourses.Queries
{
    public record ExpelStudentFromCourseQuery(EnroleDto enroleDto) : IRequest<Result<EnroleDto>>;
}
