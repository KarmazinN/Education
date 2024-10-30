using Education.Application.Common.Interfaces;
using Education.Infrastructure.Data;

namespace Education.Infrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly AppDbContext _dbContext;

        private IStudentRepository? _studentRepository;
        private ITeacherRepository? _teacherRepository;
        private ICourseRepository? _courseRepository;
        private IStudentCourseRerpository? _studentCourseRerpository;

        public IStudentRepository StudentRepository
        {
            get => _studentRepository == null ? _studentRepository = new StudentRepository(_dbContext) : _studentRepository;
        }

        public ITeacherRepository TeacherRepository
        {
            get => _teacherRepository == null ? _teacherRepository = new TeacherRepository(_dbContext) : _teacherRepository;
        }

        public ICourseRepository CourseRepository
        {
            get => _courseRepository == null ? _courseRepository = new CourseRepository(_dbContext) : _courseRepository;
        }

        public IStudentCourseRerpository StudentCourseRerpository
        {
            get => _studentCourseRerpository == null ? _studentCourseRerpository = new StudentCourseRerpository(_dbContext) : _studentCourseRerpository;
        }


        public RepositoryWrapper(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
