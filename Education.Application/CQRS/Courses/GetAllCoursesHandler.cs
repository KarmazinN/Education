using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Courses.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.CQRS.Courses
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, Result<IEnumerable<CourseDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetAllCoursesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {

            var courses = await _repositoryWrapper.CourseRepository.GetAllAsync(
                include: p => p
                    .Include(pl => pl.Teacher)
                    .Include(pl => pl.StudentCourses));

            return Result.Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }
    }
}
