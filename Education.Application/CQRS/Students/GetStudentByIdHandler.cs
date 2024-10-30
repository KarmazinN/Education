using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Students.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.CQRS.Students
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Result<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetStudentByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _repositoryWrapper
                .StudentRepository
                .GetFirstOrDefaultAsync(
                    predicate: p => p.Id == request.Id,
                    include: p => p
                        .Include(pl => pl.StudentCourses));

            if (student is null)
            {
                string errorMsg = $"Cannot find any item with corresponding id: {request.Id}";
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<StudentDto>(student));
        }
    }
}
