using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Courses.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Courses
{
    internal class DeleteCourseHandler : IRequestHandler<DeleteCourseQuery, Result<CourseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<CourseDto>> Handle(DeleteCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _repositoryWrapper.CourseRepository.GetFirstOrDefaultAsync(p => p.Id == request.id);
            if (course == null)
            {
                const string errorMsg = "No course with such id";
                return Result.Fail(errorMsg);
            }
            else
            {
                await _repositoryWrapper.CourseRepository.DeleteAsync(course.Id);
                try
                {
                    await _repositoryWrapper.SaveChangesAsync();
                    return Result.Ok();
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }
        }
    }
}
