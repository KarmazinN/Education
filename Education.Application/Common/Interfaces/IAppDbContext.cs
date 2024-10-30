using Education.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Teacher> Teachers { get; set; }
    }
}
