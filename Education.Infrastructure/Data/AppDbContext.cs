using Education.Application.Common.Interfaces;
using Education.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Courses)
                .WithMany(e => e.Students)
                .UsingEntity<StudentCourse>(
                    l => l.HasOne(sc => sc.Course)
                          .WithMany(c => c.StudentCourses)
                          .HasForeignKey(sc => sc.CourseId),
                    r => r.HasOne(sc => sc.Student)
                          .WithMany(s => s.StudentCourses)
                          .HasForeignKey(sc => sc.StudentId)
                );

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId);
        }
    }
}
