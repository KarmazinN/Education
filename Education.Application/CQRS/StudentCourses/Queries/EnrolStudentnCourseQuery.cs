using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.StudentCourses.Queries
{
    public record EnrolStudentnCourseQuery(EnroleDto enroleDto) : IRequest<Result<EnroleDto>>;
}
