using CatalogAPI.Services.Interfaces;

using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using CatalogAPI.Services.Interfaces;

namespace CatalogAPI.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetById(Guid id)
        
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> Create(CreateCourseDTO course)
        {
            var created = await _courseService.AddCourseAsync(course);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> Update(Guid id, UpdateCourseDTO course)
        {
            if (id != course.Id) return BadRequest("ID mismatch");
            var updated = await _courseService.UpdateCourseAsync(course);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
   [HttpGet("university/{universityId}")]
    public async Task<IActionResult> GetCoursesByUniversity(Guid universityId)
    {
        var courses = await _courseService.GetCoursesByUniversityIdAsync(universityId);
        if (courses == null || courses?.Count() == 0) return NotFound("No courses found for this university");
           

        return Ok(courses); // 200 OK
    }
    }

