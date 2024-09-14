using CatalogAPI.Data;
using CatalogAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses(){
        var courses = await context.Courses.ToListAsync();
        return courses;
    }

      [HttpGet("{id:int}")]
    public async Task<ActionResult<Course>> GetCourseById(int id){
        var course = await context.Courses.FindAsync(id);
        if(course == null) return NotFound();
        return course;
    }

    [HttpPost]
public async Task<ActionResult<Course>> PostCourse(Course course)
{
    context.Courses.Add(course);
    await context.SaveChangesAsync();

    //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
     return NoContent(); //success
}

[HttpPut("{id}")]
public async Task<IActionResult> PutCourse(int id, Course course)
{
    if (id != course.Id)
    {
        return BadRequest();
    }

    context.Entry(course).State = EntityState.Modified;

    try
    {
        await context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!CourseExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

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
