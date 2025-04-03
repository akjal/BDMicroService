using CatalogAPI.DTOs;

namespace  CatalogAPI.Services.Interfaces
{
   public interface IUniversityService
    {
        Task<IEnumerable<UniversityDTO>> GetAllUniversitiesAsync();
        Task<UniversityDTO?> GetUniversityByIdAsync(Guid id);
        Task<UniversityDTO> AddUniversityAsync(CreateUniversityDTO dto);
        Task<UniversityDTO?> UpdateUniversityAsync(UpdateUniversityDTO dto);
        Task<bool> DeleteUniversityAsync(Guid id);
    }
}