using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.StudentCourses.Queries;
using Education.Domain.Common.Models;
using FluentResults;
using MediatR;


namespace Education.Application.CQRS.StudentCourses
{
    internal class EnrolStudentnCourseHandler : IRequestHandler<EnrolStudentnCourseQuery, Result<EnroleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public EnrolStudentnCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<EnroleDto>> Handle(EnrolStudentnCourseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var studentCourse = _mapper.Map<StudentCourse>(request.enroleDto);

                var existingItem = await _repositoryWrapper.StudentCourseRerpository
                    .GetFirstOrDefaultAsync(x => x.CourseId == request.enroleDto.CourseId && x.StudentId == request.enroleDto.StudentId);

                var student = await _repositoryWrapper.StudentRepository.GetFirstOrDefaultAsync(x => x.Id == request.enroleDto.StudentId);
                var course = await _repositoryWrapper.CourseRepository.GetFirstOrDefaultAsync(x => x.Id == request.enroleDto.CourseId);

                if (existingItem is not null)
                {
                    string errorMsg = $"A item with this fields already exists";
                    return Result.Fail(new Error(errorMsg));
                }

                if (student == null || course == null)
                {
                    string errorMsg = "Student oe course is not valid";
                    return Result.Fail(new Error(errorMsg));
                }

                studentCourse.CourseId = course.Id;
                studentCourse.StudentId = student.Id;

                await _repositoryWrapper.StudentCourseRerpository.AddAsync(studentCourse);
                await _repositoryWrapper.SaveChangesAsync();

                var studentDto = _mapper.Map<EnroleDto>(studentCourse);

                return Result.Ok(studentDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<EnroleDto>(ex.Message);
            }
        }
    }
}
