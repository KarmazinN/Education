using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Teachers.Queries
{
    public record GetAllTeachersQuery() : IRequest<Result<IEnumerable<TeacherDto>>>;
}
