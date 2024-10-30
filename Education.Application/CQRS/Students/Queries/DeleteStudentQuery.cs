using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students.Queries
{
    public record DeleteStudentQuery(int id) : IRequest<Result<StudentDto>>;
}
