using Education.Application.Common.Interfaces;
using Education.Domain.Common.Models;
using Education.Infrastructure.Data;

namespace Education.Infrastructure.Repositories
{
    public class StudentRepository(AppDbContext dbContext) : RepositoryBase<Student>(dbContext), IStudentRepository
    {
    }
}
