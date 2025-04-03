using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CatalogAPI.Services.Implementations
{
   public class CourseService : ICourseService
    {
        private readonly DataContext _context;

        public CourseService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
                return await _context.Courses
        .Include(c => c.University)
        .Select(c => new CourseDTO
        {
            Id = c.Id,
            Name = c.Name,
            TuitionFee = c.TuitionFee,
            Duration = c.Duration,
            IntakeMonths = c.IntakeMonths,
            UniversityId = c.UniversityId,
            MaxEnrollment = c.MaxEnrollment,
            CurrentEnrollment = c.CurrentEnrollment,
            UniversityName = c.University.Name
        })
        .ToListAsync();


        }

        public async Task<CourseDTO?> GetCourseByIdAsync(Guid id)
        {
               return await _context.Courses
        .Include(c => c.University)
        .Where(c => c.Id == id)
        .Select(c => new CourseDTO
        {
            Id = c.Id,
            Name = c.Name,
            TuitionFee = c.TuitionFee,
            Duration = c.Duration,
            IntakeMonths = c.IntakeMonths,
             MaxEnrollment = c.MaxEnrollment,
            CurrentEnrollment = c.CurrentEnrollment,
            UniversityId = c.UniversityId,
            UniversityName = c.University.Name
        })
        .FirstOrDefaultAsync();

        }

        public async Task<CourseDTO> AddCourseAsync(CreateCourseDTO dto)
        {
         var course = new Course
    {
        Id = Guid.NewGuid(),
        Name = dto.Name,
        TuitionFee = dto.TuitionFee,
        Duration = dto.Duration,
        MaxEnrollment = dto.MaxEnrollment,
        CurrentEnrollment = dto.CurrentEnrollment,
        IntakeMonths = dto.IntakeMonths,
        UniversityId = dto.UniversityId
    };

    _context.Courses.Add(course);
    await _context.SaveChangesAsync();

    // Fetch back with University info
    var university = await _context.Universities.FindAsync(dto.UniversityId);
    return new CourseDTO
    {
        Id = course.Id,
        Name = course.Name,
        TuitionFee = course.TuitionFee,
        Duration = course.Duration,
        IntakeMonths = course.IntakeMonths,
        MaxEnrollment = course.MaxEnrollment,
        CurrentEnrollment = course.CurrentEnrollment,
        UniversityId = course.UniversityId,
        UniversityName = university?.Name
    };

        }

        public async Task<CourseDTO> UpdateCourseAsync(UpdateCourseDTO dto)
        {
                var course = await _context.Courses.FindAsync(dto.Id);
    if (course == null) return null;

    course.Name = dto.Name;
    course.TuitionFee = dto.TuitionFee;
    course.Duration = dto.Duration;
    course.IntakeMonths = dto.IntakeMonths;
    course.MaxEnrollment = dto.MaxEnrollment;
    course.CurrentEnrollment = dto.CurrentEnrollment;
    course.UniversityId = dto.UniversityId;

    await _context.SaveChangesAsync();

    var university = await _context.Universities.FindAsync(dto.UniversityId);
    return new CourseDTO
    {
        Id = course.Id,
        Name = course.Name,
        TuitionFee = course.TuitionFee,
        Duration = course.Duration,
        IntakeMonths = course.IntakeMonths,
        MaxEnrollment = course.MaxEnrollment,
        CurrentEnrollment = course.CurrentEnrollment,
        UniversityId = course.UniversityId,
        UniversityName = university?.Name
    };

        }

        public async Task<bool> DeleteCourseAsync(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}