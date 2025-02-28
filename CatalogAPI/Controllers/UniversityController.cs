using CatalogAPI.Data;
using CatalogAPI.Entities;
using CatalogAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UniversitiesController(DataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<University>>> GetCourses(){
        var universities = await context.Universities.ToListAsync();
        return universities;
    }
}