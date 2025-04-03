 using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
namespace CatalogAPI.Services.Interfaces;


 public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync();
        Task<ApplicationDTO> GetApplicationByIdAsync(Guid id);
        Task<ApplicationDTO> AddApplicationAsync(CreateApplicationDTO dto);
        Task<ApplicationDTO?> UpdateApplicationAsync(UpdateApplicationDTO dto);
        Task<bool> DeleteApplicationAsync(Guid id);
    }