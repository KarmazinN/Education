using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Teachers.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.CQRS.Teachers
{
    public class GetAllTeachersHandler : IRequestHandler<GetAllTeachersQuery, Result<IEnumerable<TeacherDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetAllTeachersHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<TeacherDto>>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {

            var students = await _repositoryWrapper.TeacherRepository.GetAllAsync(
                include: p => p
                    .Include(pl => pl.Courses));
            return Result.Ok(_mapper.Map<IEnumerable<TeacherDto>>(students));
        }
    }
}
