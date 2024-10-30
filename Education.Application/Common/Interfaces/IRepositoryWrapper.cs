namespace Education.Application.Common.Interfaces
{
    public interface IRepositoryWrapper
    {
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        ICourseRepository CourseRepository { get; }
        IStudentCourseRerpository StudentCourseRerpository { get; }


        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }
}
