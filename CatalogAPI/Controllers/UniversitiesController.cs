using CatalogAPI.Data;
using CatalogAPI.Entities;
using CatalogAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversitiesController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<University>>> GetUniversities(){
        var universities = await context.Universities.ToListAsync();
        return universities;
    }

     [HttpGet("{id:int}")]
    public async Task<ActionResult<University>> GetUniversityById(int id){
        var uni = await context.Universities.FindAsync(id);
        if(uni == null) return NotFound();
        return uni;
    }

    [HttpPost]
public async Task<ActionResult<University>> PostCourse(University uni)
{
    context.Universities.Add(uni);
    await context.SaveChangesAsync();

    //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
     return NoContent(); //success
}

[HttpPut("{id}")]
public async Task<IActionResult> PutCourse(int id, University uni)
{
    if (id != uni.Id)
    {
        return BadRequest();
    }

    context.Entry(uni).State = EntityState.Modified;

    try
    {
        await context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!UniversityExists(id))
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
private bool UniversityExists(int id)
    {
        return context.Universities.Any(e => e.Id == id);
    }
}