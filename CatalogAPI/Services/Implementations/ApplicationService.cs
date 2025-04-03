using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;
namespace CatalogAPI.Services.Interfaces;
public class ApplicationService : IApplicationService
    {
        private readonly DataContext _context;

        public ApplicationService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync()
        {
            return await _context.Applications
                .Include(a => a.Student)
                .Include(a => a.Course)
                    .ThenInclude(c => c.University)
                .Select(a => new ApplicationDTO
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    StudentName = a.Student.FirstName + " " + a.Student.LastName,
                    CourseId = a.CourseId,
                    CourseName = a.Course.Name,
                    UniversityId = a.Course.UniversityId,
                    UniversityName = a.Course.University.Name,
                    ApplicationStatus = a.ApplicationStatus,
                    CreatedOn = a.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<ApplicationDTO?> GetApplicationByIdAsync(Guid id)
        {
            return await _context.Applications
                .Include(a => a.Student)
                .Include(a => a.Course)
                    .ThenInclude(c => c.University)
                .Where(a => a.Id == id)
                .Select(a => new ApplicationDTO
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    StudentName = a.Student.FirstName + " " + a.Student.LastName,
                    CourseId = a.CourseId,
                    CourseName = a.Course.Name,
                    UniversityId = a.Course.UniversityId,
                    UniversityName = a.Course.University.Name,
                    ApplicationStatus = a.ApplicationStatus,
                    CreatedOn = a.CreatedOn
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ApplicationDTO> AddApplicationAsync(CreateApplicationDTO dto)
        {
            var application = new Application
            {
                Id = Guid.NewGuid(),
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                ApplicationStatus = dto.ApplicationStatus,
                CreatedOn = DateTime.UtcNow
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            var course = await _context.Courses.Include(c => c.University)
                                               .FirstOrDefaultAsync(c => c.Id == dto.CourseId);
            var student = await _context.Students.FindAsync(dto.StudentId);

            return new ApplicationDTO
            {
                Id = application.Id,
                StudentId = application.StudentId,
                StudentName = student.FirstName + " " + student.LastName,
                CourseId = application.CourseId,
                CourseName = course?.Name,
                UniversityId = course?.UniversityId ?? Guid.Empty,
                UniversityName = course?.University?.Name,
                ApplicationStatus = application.ApplicationStatus,
                CreatedOn = application.CreatedOn
            };
        }

        public async Task<ApplicationDTO?> UpdateApplicationAsync(UpdateApplicationDTO dto)
        {
            var application = await _context.Applications.FindAsync(dto.Id);
            if (application == null) return null;

            application.StudentId = dto.StudentId;
            application.CourseId = dto.CourseId;
            application.ApplicationStatus = dto.ApplicationStatus;
            await _context.SaveChangesAsync();

            var course = await _context.Courses.Include(c => c.University)
                                               .FirstOrDefaultAsync(c => c.Id == dto.CourseId);
            var student = await _context.Students.FindAsync(dto.StudentId);

            return new ApplicationDTO
            {
                Id = application.Id,
                StudentId = application.StudentId,
                StudentName = student.FirstName + " " + student.LastName,
                CourseId = application.CourseId,
                CourseName = course?.Name,
                UniversityId = course?.UniversityId ?? Guid.Empty,
                UniversityName = course?.University?.Name,
                ApplicationStatus = application.ApplicationStatus,
                CreatedOn = application.CreatedOn
            };
        }

        public async Task<bool> DeleteApplicationAsync(Guid id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null) return false;

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return true;
        }
    }
