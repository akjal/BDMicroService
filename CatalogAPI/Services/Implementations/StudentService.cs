using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CatalogAPI.Services.Implementations;

    public class StudentService : IStudentService
    {
        private readonly DataContext _context;

        public StudentService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            return await _context.Students.Select(s=> new StudentDTO{
                FirstName = s.FirstName,
                LastName = s.LastName,Address = s.Address,
                DateOfBirth = s.DateOfBirth,Email = s.Email,
                Nationality = s.Nationality,
                ContactNumber = s.ContactNumber,
                PassportNumber = s.PassportNumber,
                PassportIssueDate = s.PassportIssueDate,
                PassportExpiryDate = s.PassportExpiryDate   
            
            }).ToListAsync();
        }

        public async Task<StudentDTO> GetStudentByIdAsync(Guid id)
        {
            return await _context.Students.Where(s=>s.Id == id)
            .Select(s=> new StudentDTO{
                FirstName = s.FirstName,
                LastName = s.LastName,Address = s.Address,
                DateOfBirth = s.DateOfBirth,Email = s.Email,
                Nationality = s.Nationality,
                ContactNumber = s.ContactNumber,
                PassportNumber = s.PassportNumber,
                PassportIssueDate = s.PassportIssueDate,
                PassportExpiryDate = s.PassportExpiryDate   
            
            }).FirstOrDefaultAsync();
        }

        public async Task<StudentDTO> AddStudentAsync(StudentDTO studentDto)
        {
              var student = new Student
                {
                    Id = Guid.NewGuid(),
                    FirstName = studentDto.FirstName,
                    LastName = studentDto.LastName,
                    Email = studentDto.Email,
                    DateOfBirth = studentDto.DateOfBirth,
                    Nationality = studentDto.Nationality,
                    ContactNumber = studentDto.ContactNumber,
                    Address = studentDto.Address,
                    PassportNumber = studentDto.PassportNumber,
                    PassportIssueDate = studentDto.PassportIssueDate,
                    PassportExpiryDate = studentDto.PassportExpiryDate
                };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

           var addedStudent = await _context.Students.FindAsync(studentDto.Id);
           return new StudentDTO
                {
                    FirstName = addedStudent.FirstName,
                    LastName = addedStudent.LastName,
                    Email = addedStudent.Email,
                    DateOfBirth = addedStudent.DateOfBirth,
                    Nationality = addedStudent.Nationality,
                    ContactNumber = addedStudent.ContactNumber,
                    Address = addedStudent.Address,
                    PassportNumber = addedStudent.PassportNumber,
                    PassportIssueDate = addedStudent.PassportIssueDate,
                    PassportExpiryDate = addedStudent.PassportExpiryDate
                };
        }

        public async Task<StudentDTO> UpdateStudentAsync(StudentDTO student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var updatedStudent = await _context.Students.FindAsync(student.Id);
            if (updatedStudent == null) return null;
            return new StudentDTO
                {
                    FirstName = updatedStudent.FirstName,
                    LastName = updatedStudent.LastName,
                    Email = updatedStudent.Email,
                    DateOfBirth = updatedStudent.DateOfBirth,
                    Nationality = updatedStudent.Nationality,
                    ContactNumber = updatedStudent.ContactNumber,
                    Address = updatedStudent.Address,
                    PassportNumber = updatedStudent.PassportNumber,
                    PassportIssueDate = updatedStudent.PassportIssueDate,
                    PassportExpiryDate = updatedStudent.PassportExpiryDate
                }; 
            return student;
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
            
        }
    }

  