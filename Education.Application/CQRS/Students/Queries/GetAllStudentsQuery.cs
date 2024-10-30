using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students.Queries
{
    public record GetAllStudentsQuery() : IRequest<Result<IEnumerable<StudentDto>>>;
}
