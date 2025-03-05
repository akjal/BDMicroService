using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(DataContext context) : ControllerBase
{
    [HttpGet]
public async Task<IActionResult> GetAllCourses()
{            var courses = await context.Courses
        .Include(c => c.CourseDetails)
        .Select(c => new CourseDTO
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Duration = c.CourseDetails != null ? c.CourseDetails.Duration : null,
            Type = c.CourseDetails != null ? c.CourseDetails.CourseType : null,
            CurrentEnrollment = c.CourseDetails != null ? c.CourseDetails.CurrentEnrollment : 0,    
            MaxEnrollment = c.CourseDetails != null ? c.CourseDetails.MaxEnrollment : 0

        })
        .ToListAsync();
       

    if (courses == null || courses.Count == 0)
    {
        return NotFound("No courses found");
    }

    return Ok(courses);
    }

      [HttpGet("{id:int}")]
    public async Task<ActionResult<Course>> GetCourseById(int id){
        var course = await context.Courses.FindAsync(id);
        if(course == null) return NotFound();
        return course;
    }

    [HttpPost]
public async Task<ActionResult<Course>> PostCourse(CourseDTO courseDto)
{

      var course = new Course
        {
            Name = courseDto.Name,
            Description = courseDto.Description,
            CourseDetails = new CourseDetails
            {
                MaxEnrollment = courseDto.MaxEnrollment,
                CurrentEnrollment = courseDto.CurrentEnrollment,
                CourseType = courseDto.Type,
                Duration = courseDto.Duration
            }
        };
    context.Courses.Add(course);
    await context.SaveChangesAsync();

    //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
     return NoContent(); //success
}

[HttpPut("{id}")]
public async Task<IActionResult> PutCourse(int id, CourseDTO courseDto)
{
    if (id != courseDto.Id)
    {
        return BadRequest();
    }
        var existingCourse = await context.Courses
        .Include(c => c.CourseDetails)
        .FirstOrDefaultAsync(c => c.Id == id);

    if (existingCourse == null)
    {
        return NotFound("Course not found");
    }
    
        // Update Course Fields
    existingCourse.Name = courseDto.Name;
    existingCourse.Description = courseDto.Description;
    // Update CourseDetails Fields
    if (existingCourse.CourseDetails != null)
    {
        existingCourse.CourseDetails.CourseType = courseDto.Type;
        existingCourse.CourseDetails.Duration = courseDto.Duration;
        existingCourse.CourseDetails.MaxEnrollment = courseDto.MaxEnrollment;
        existingCourse.CourseDetails.CurrentEnrollment = courseDto.CurrentEnrollment;   
    }
    else
    {
        // Create new CourseDetails if it does not exist
        existingCourse.CourseDetails = new CourseDetails
        {
             MaxEnrollment = courseDto.MaxEnrollment,
                CurrentEnrollment = courseDto.CurrentEnrollment,
                CourseType = courseDto.Type,
                Duration = courseDto.Duration
        };
    }

    await context.SaveChangesAsync(); // Save changes to DB

    // context.Entry(courseDto).State = EntityState.Modified;

    // try
    // {
    //     await context.SaveChangesAsync();
    // }
    // catch (DbUpdateConcurrencyException)
    // {
    //     if (!CourseExists(id))
    //     {
    //         return NotFound();
    //     }
    //     else
    //     {
    //         throw;
    //     }
    // }

    return NoContent();
}
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTodoItem(int id)
{
    var currentCourse = await context.Courses.FindAsync(id);
    if (currentCourse == null)
    {
        return NotFound();
    }

    context.Courses.Remove(currentCourse);
    await context.SaveChangesAsync();

    return NoContent();
}
private bool CourseExists(int id)
    {
        return context.Courses.Any(e => e.Id == id);
    }

}
