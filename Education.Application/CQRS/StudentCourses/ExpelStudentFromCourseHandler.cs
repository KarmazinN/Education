using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.StudentCourses.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.StudentCourses
{
    public class ExpelStudentFromCourseHandler : IRequestHandler<ExpelStudentFromCourseQuery, Result<EnroleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ExpelStudentFromCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<EnroleDto>> Handle(ExpelStudentFromCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _repositoryWrapper.StudentCourseRerpository.
                GetFirstOrDefaultAsync(x => x.CourseId == request.enroleDto.CourseId && x.StudentId == request.enroleDto.StudentId);

            if (course == null)
            {
                const string errorMsg = "No course with such id";
                return Result.Fail(errorMsg);
            }
            else
            {
                await _repositoryWrapper.StudentCourseRerpository.DeleteAsync(course.Id);
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
