using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
namespace CatalogAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(Guid id);
        Task<StudentDTO> AddStudentAsync(StudentDTO student);
        Task<StudentDTO> UpdateStudentAsync(StudentDTO student);
        Task<bool> DeleteStudentAsync(Guid id);
    }
}
