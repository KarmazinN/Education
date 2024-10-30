using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Courses.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.CQRS.Courses
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetCourseByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _repositoryWrapper
                .CourseRepository
                .GetFirstOrDefaultAsync(
                    predicate: p => p.Id == request.Id,
                    include: p => p
                        .Include(pl => pl.StudentCourses));

            return Result.Ok(_mapper.Map<CourseDto>(course));
        }
    }
}
