using AutoMapper;
using Education.Application.Common.DTOs;
using Education.Domain.Common.Models;

namespace Education.Application.Common.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Teacher, CreateTeacherDto>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();

            CreateMap<StudentCourse, EnroleDto>().ReverseMap();
        }    
    }
}
