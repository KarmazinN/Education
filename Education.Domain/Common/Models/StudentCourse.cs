﻿namespace Education.Domain.Common.Models
{
    public class StudentCourse
    {
        public int Id { get; set; } 
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
