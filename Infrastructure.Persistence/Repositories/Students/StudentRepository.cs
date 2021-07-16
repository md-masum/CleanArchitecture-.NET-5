using System.Linq;
using System.Threading.Tasks;
using Application.Common.Paging;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Students
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<PagedList<Student>> GetAllStudent(int pageNumber, int pageSize)
        {
            var students = GetAsQueryable();
            return await PagedList<Student>.CreateAsync(students, pageNumber, pageSize);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<Student> CreateStudent(Student student)
        {
            await AddAsync(student);
            return student;
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            await UpdateAsync(student);
            return student;
        }

        public async Task<bool> DeleteStudent(Student student)
        {
            return await RemoveAsync(student);
        }

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(c => c.Email == email);
            return student is null;
        }

        public async Task<bool> IsUniqueEmailForUpdateAsync(int id, string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(c => c.Email == email && c.Id != id);
            return student is null;
        }
    }
}
