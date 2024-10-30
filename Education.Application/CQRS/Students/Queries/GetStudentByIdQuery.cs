using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students.Queries
{
    public record GetStudentByIdQuery(int Id) : IRequest<Result<StudentDto>>;
}
