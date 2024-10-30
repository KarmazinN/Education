using Education.Domain.Common.Models;

namespace Education.Application.Common.DTOs
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int TeacherId { get; set; }
    }
}
