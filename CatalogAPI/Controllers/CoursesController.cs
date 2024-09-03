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
public async Task<ActionResult<Course>> PostTodoItem(Course course)
{
    context.Courses.Add(course);
    await context.SaveChangesAsync();

    //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
     return NoContent(); //success
}
}
