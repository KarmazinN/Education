﻿using Education.Application.Common.DTOs;
using FluentResults;
using MediatR;

namespace Education.Application.CQRS.Teachers.Queries
{
    public record DeleteTeacherQuery(int id) : IRequest<Result<TeacherDto>>;
}