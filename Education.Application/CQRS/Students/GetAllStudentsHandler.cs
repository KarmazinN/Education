using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Students.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.CQRS.Students
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, Result<IEnumerable<StudentDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetAllStudentsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {

            var students = await _repositoryWrapper.StudentRepository.GetAllAsync(
                include: p => p
                    .Include(pl => pl.StudentCourses));
            return Result.Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
        }
    }
}
