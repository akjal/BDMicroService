
using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services.Implementations
{
    public class UniversityService : IUniversityService
    {
        private readonly DataContext _context;

        public UniversityService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UniversityDTO>> GetAllUniversitiesAsync()
        {
            return await _context.Universities
                .Select(u => new UniversityDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    City = u.City,
                    Country = u.Country,
                    Website = u.Website,
                    ContactEmail = u.ContactEmail
                }).ToListAsync();
        }

        public async Task<UniversityDTO?> GetUniversityByIdAsync(Guid id)
        {
            return await _context.Universities
                .Where(u => u.Id == id)
                .Select(u => new UniversityDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    City = u.City,
                    Country = u.Country,
                    Website = u.Website,
                    ContactEmail = u.ContactEmail
                }).FirstOrDefaultAsync();
        }

        public async Task<UniversityDTO> AddUniversityAsync(CreateUniversityDTO dto)
        {
            var university = new University
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                City = dto.City,
                Country = dto.Country,
                Website = dto.Website,
                ContactEmail = dto.ContactEmail
            };

            _context.Universities.Add(university);
            await _context.SaveChangesAsync();

            return new UniversityDTO
            {
                Id = university.Id,
                Name = university.Name,
                City = university.City,
                Country = university.Country,
                Website = university.Website,
                ContactEmail = university.ContactEmail
            };
        }

        public async Task<UniversityDTO?> UpdateUniversityAsync(UpdateUniversityDTO dto)
        {
            var university = await _context.Universities.FindAsync(dto.Id);
            if (university == null) return null;

            university.Name = dto.Name;
            university.City = dto.City;
            university.Country = dto.Country;
            university.Website = dto.Website;
            university.ContactEmail = dto.ContactEmail;

            await _context.SaveChangesAsync();

            return new UniversityDTO
            {
                Id = university.Id,
                Name = university.Name,
                City = university.City,
                Country = university.Country,
                Website = university.Website,
                ContactEmail = university.ContactEmail
            };
        }

        public async Task<bool> DeleteUniversityAsync(Guid id)
        {
            var university = await _context.Universities.FindAsync(id);
            if (university == null) return false;

            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
