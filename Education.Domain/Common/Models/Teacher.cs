namespace Education.Domain.Common.Models
{
    public class Teacher : Person
    {
        public ICollection<Course> Courses { get; set; } = [];
    }
}
