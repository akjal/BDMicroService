using CatalogAPI.Data;
using CatalogAPI.DTOs;
using CatalogAPI.Entities;
using CatalogAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogAPI.Services.Interfaces;
using System.Text.Json;

namespace CatalogAPI.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
            private readonly IStudentService _studentService;
        private readonly IApplicationService _applicationService;
         private readonly IDocumentService _documentService;

        public ApplicationController(
            IStudentService studentService,
            IApplicationService applicationService,
            IDocumentService documentService)
        {
            _studentService = studentService;
            _applicationService = applicationService;
            _documentService = documentService;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAll()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetById(Guid id)
        {
            var app = await _applicationService.GetApplicationByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationDTO>> Create(CreateApplicationDTO dto)
        {
            var created = await _applicationService.AddApplicationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationDTO>> Update(Guid id, UpdateApplicationDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");
            var updated = await _applicationService.UpdateApplicationAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _applicationService.DeleteApplicationAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

                [HttpPost("submit")]
        public async Task<IActionResult> SubmitApplication([FromForm] ApplicationSubmissionDTO dto)
        {
            try
            {
                // Step 1: Deserialize Student & Application data
                var studentDto = JsonSerializer.Deserialize<StudentDTO>(dto.StudentJson);
                var applicationDto = JsonSerializer.Deserialize<CreateApplicationDTO>(dto.ApplicationJson);

                // Step 2: Save Student
                var student = await _studentService.AddStudentAsync(studentDto);

                // Step 3: Save Application
                 var applicationId = await _applicationService.AddApplicationAsync(applicationDto);


                // Step 4: Save uploaded documents
               

                return Ok(new { Message = "Application submitted successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Failed to submit application", Error = ex.Message });
            }
        }

    }
