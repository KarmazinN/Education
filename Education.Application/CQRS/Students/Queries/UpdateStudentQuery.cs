using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students.Queries
{
    public record UpdateStudentQuery(UpdateStudentDto updateStudentDto) : IRequest<Result<UpdateStudentDto>>;
}
