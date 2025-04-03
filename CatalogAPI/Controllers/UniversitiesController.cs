using CatalogAPI.Services.Interfaces;
using CatalogAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityDTO>>> GetAll()
        {
            var result = await _universityService.GetAllUniversitiesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UniversityDTO>> GetById(Guid id)
        {
            var university = await _universityService.GetUniversityByIdAsync(id);
            if (university == null) return NotFound();
            return Ok(university);
        }

        [HttpPost]
        public async Task<ActionResult<UniversityDTO>> Create(CreateUniversityDTO dto)
        {
            var created = await _universityService.AddUniversityAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UniversityDTO>> Update(Guid id, UpdateUniversityDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            var updated = await _universityService.UpdateUniversityAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _universityService.DeleteUniversityAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
