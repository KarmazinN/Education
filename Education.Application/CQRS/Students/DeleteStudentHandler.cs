using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Students.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Students
{
    internal class DeleteStudentHandler : IRequestHandler<DeleteStudentQuery, Result<StudentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteStudentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<StudentDto>> Handle(DeleteStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _repositoryWrapper.StudentRepository.GetFirstOrDefaultAsync(p => p.Id == request.id);
            if (student == null)
            {
                const string errorMsg = "No studednt with such id";
                return Result.Fail(errorMsg);
            }
            else
            {
                await _repositoryWrapper.StudentRepository.DeleteAsync(student.Id);
                try
                {
                    await _repositoryWrapper.SaveChangesAsync();

                    var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

                    if (resultIsSuccess)
                    {
                        return Result.Ok();
                    }
                    else
                    {

                        string errorMsg = $"Cannot find any item with corresponding id: {request.id}";
                        return Result.Fail(new Error(errorMsg));
                    }
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }
            }
        }
    }
}
