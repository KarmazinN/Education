using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Students.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentQuery, Result<UpdateStudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateStudentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<UpdateStudentDto>> Handle(UpdateStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var student = await _repositoryWrapper.StudentRepository.GetFirstOrDefaultAsync(x => x.Id == request.updateStudentDto.Id);

                if (student == null)
                {
                    return Result.Fail<UpdateStudentDto>($"Student with Id {request.updateStudentDto.Id} not found.");
                }

                student.FirstName = request.updateStudentDto.FirstName;
                student.LastName = request.updateStudentDto.LastName;
                student.Email = request.updateStudentDto.Email;

                await _repositoryWrapper.StudentRepository.UpdateAsync(student);
                await _repositoryWrapper.SaveChangesAsync();

                var studentDto = _mapper.Map<UpdateStudentDto>(student);

                return Result.Ok(studentDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<UpdateStudentDto>(ex.Message);
            }

        }
    }
}
