using Education.Application.Common.Interfaces;
using Education.Domain.Common.Models;
using Education.Infrastructure.Data;

namespace Education.Infrastructure.Repositories
{
    public class TeacherRepository(AppDbContext dbContext) : RepositoryBase<Teacher>(dbContext), ITeacherRepository
    {
    }
}
