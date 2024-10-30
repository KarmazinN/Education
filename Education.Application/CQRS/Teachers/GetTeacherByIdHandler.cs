using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Application.Common.Interfaces;
using Education.Application.CQRS.Teachers.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.CQRS.Teachers
{
    public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, Result<TeacherDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetTeacherByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<TeacherDto>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _repositoryWrapper
                .TeacherRepository
                .GetFirstOrDefaultAsync(
                    predicate: p => p.Id == request.Id,
                    include: p => p
                        .Include(pl => pl.Courses));

            if (teacher is null)
            {
                string errorMsg = $"No teacher by entered Id - {request.Id}";
                return Result.Fail<TeacherDto>(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<TeacherDto>(teacher));
        }
    }
}
