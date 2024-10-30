using Education.Domain.Common.Models;
using System.ComponentModel.DataAnnotations;


namespace Education.Application.Common.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = [];
    }
}
