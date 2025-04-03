using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
namespace CatalogAPI.Services.Interfaces
{

    public interface ICourseService
    {
        Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        Task<CourseDTO?> GetCourseByIdAsync(Guid id);
        Task<CourseDTO> AddCourseAsync(CreateCourseDTO course);
        Task<CourseDTO> UpdateCourseAsync(UpdateCourseDTO course);
        Task<bool> DeleteCourseAsync(Guid id);
    }
}
