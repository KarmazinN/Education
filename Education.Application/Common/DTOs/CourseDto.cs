using Education.Domain.Common.Models;

namespace Education.Application.Common.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = [];
    }
}
