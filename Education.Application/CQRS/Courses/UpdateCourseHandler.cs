using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Courses.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Courses
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseQuery, Result<UpdateCourseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<UpdateCourseDto>> Handle(UpdateCourseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _repositoryWrapper.CourseRepository.GetFirstOrDefaultAsync(x => x.Id == request.courseDto.Id);

                if (course == null)
                {
                    return Result.Fail<UpdateCourseDto>($"Course with Id {request.courseDto.Id} not found.");
                }

                course.Title = request.courseDto.Title;
                course.TeacherId = request.courseDto.TeacherId;
                course.Teacher = await _repositoryWrapper.TeacherRepository.GetFirstOrDefaultAsync(x => x.Id == request.courseDto.Id);

                await _repositoryWrapper.CourseRepository.UpdateAsync(course);
                await _repositoryWrapper.SaveChangesAsync();

                var courseDto = _mapper.Map<UpdateCourseDto>(course);

                return Result.Ok(courseDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<UpdateCourseDto>(ex.Message);
            }

        }
    }
}
