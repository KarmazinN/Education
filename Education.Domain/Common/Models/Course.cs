namespace Education.Domain.Common.Models
{
    public class Course
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
