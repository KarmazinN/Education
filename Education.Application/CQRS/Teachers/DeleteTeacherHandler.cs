using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Teachers.Queries;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Teachers
{
    internal class DeleteTeacherHandler : IRequestHandler<DeleteTeacherQuery, Result<TeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteTeacherHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<TeacherDto>> Handle(DeleteTeacherQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _repositoryWrapper.TeacherRepository.GetFirstOrDefaultAsync(p => p.Id == request.id);
            if (teacher == null)
            {
                const string errorMsg = "No teacher with such id";
                return Result.Fail(errorMsg);
            }
            else
            {
                try
                {
                    await _repositoryWrapper.TeacherRepository.DeleteAsync(teacher.Id);
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
