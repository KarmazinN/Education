using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Students.Queries;
using Education.Domain.Common.Models;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students
{
    public class CreateCourseHandler : IRequestHandler<CreateStudentQuery, Result<CreateStudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateCourseHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<CreateStudentDto>> Handle(CreateStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = _mapper.Map<Student>(request.newStudent);

                var existingEmail = await _repositoryWrapper.StudentRepository.GetFirstOrDefaultAsync(x => x.Email == student.Email);
                if(existingEmail is not null)
                {
                    string errorMsg = $"A student with this email {student.Email} already exists";
                    return Result.Fail(new Error(errorMsg));
                }

                await _repositoryWrapper.StudentRepository.AddAsync(student);
                await _repositoryWrapper.SaveChangesAsync();

                var studentDto = _mapper.Map<CreateStudentDto>(student);

                return Result.Ok(studentDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<CreateStudentDto>(ex.Message);
            }
        }
    }
}
