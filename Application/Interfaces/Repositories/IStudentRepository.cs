using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Paging;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStudentRepository : IDisposable
    {
        Task<PagedList<Student>> GetAllStudent(int pageNumber, int pageSize);
        Task<Student> GetStudentById(int id);
        Task<Student> CreateStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<bool> DeleteStudent(Student student);
        Task<bool> IsUniqueEmailAsync(string email);
        Task<bool> IsUniqueEmailForUpdateAsync(int id, string email);
    }
}
