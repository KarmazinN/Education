using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Courses.Queries;
using Education.Domain.Common.Models;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Courses
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseQuery, Result<CreateCourseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<CreateCourseDto>> Handle(CreateCourseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = _mapper.Map<Course>(request.newCourse);

                var existingEmail = await _repositoryWrapper.CourseRepository.GetFirstOrDefaultAsync(x => x.Title == course.Title);
                if(existingEmail is not null)
                {
                    string errorMsg = $"A student with this title {course.Title} already exists";
                    return Result.Fail(new Error(errorMsg));
                }

                await _repositoryWrapper.CourseRepository.AddAsync(course);
                await _repositoryWrapper.SaveChangesAsync();

                var studentDto = _mapper.Map<CreateCourseDto>(course);

                return Result.Ok(studentDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<CreateCourseDto>(ex.Message);
            }
        }
    }
}
