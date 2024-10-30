using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Teachers.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Teachers
{
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherQuery, Result<UpdateTeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateTeacherHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<UpdateTeacherDto>> Handle(UpdateTeacherQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = await _repositoryWrapper.TeacherRepository.GetFirstOrDefaultAsync(x => x.Id == request.teacherDto.Id);

                if (teacher == null)
                {
                    return Result.Fail<UpdateTeacherDto>($"Student with Id {request.teacherDto.Id} not found.");
                }

                teacher.FirstName = request.teacherDto.FirstName;
                teacher.LastName = request.teacherDto.LastName;
                teacher.Email = request.teacherDto.Email;

                await _repositoryWrapper.TeacherRepository.UpdateAsync(teacher);
                await _repositoryWrapper.SaveChangesAsync();

                var teacherDto = _mapper.Map<UpdateTeacherDto>(teacher);

                return Result.Ok(teacherDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<UpdateTeacherDto>(ex.Message);
            }

        }
    }
}
