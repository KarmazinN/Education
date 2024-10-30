using Education.Application.Common.Interfaces;
using Education.Domain.Common.Models;
using Education.Infrastructure.Data;

namespace Education.Infrastructure.Repositories
{
    public class CourseRepository(AppDbContext dbContext) : RepositoryBase<Course>(dbContext), ICourseRepository
    {
    }
}
