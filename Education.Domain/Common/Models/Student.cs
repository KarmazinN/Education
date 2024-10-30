namespace Education.Domain.Common.Models
{
    public class Student : Person
    {
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
