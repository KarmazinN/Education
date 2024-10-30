using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Teachers.Queries;
using Education.Domain.Common.Models;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Teachers
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherQuery, Result<TeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateTeacherHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<TeacherDto>> Handle(CreateTeacherQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = _mapper.Map<Teacher>(request.newTeacher);

                var existingEmail = await _repositoryWrapper.TeacherRepository.GetFirstOrDefaultAsync(x => x.Email == teacher.Email);
                if (existingEmail is not null)
                {
                    string errorMsg = $"A teacher with this email {teacher.Email} already exists";
                    return Result.Fail(new Error(errorMsg));
                }

                await _repositoryWrapper.TeacherRepository.AddAsync(teacher);
                await _repositoryWrapper.SaveChangesAsync();

                var teacherDto = _mapper.Map<TeacherDto>(teacher);

                return Result.Ok(teacherDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<TeacherDto>(ex.Message);
            }
        }
    }
}
