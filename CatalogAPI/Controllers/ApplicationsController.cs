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
         private readonly DocumentIntelligenceService _docExtractService;

        public ApplicationController(
            IStudentService studentService,
            IApplicationService applicationService,
            IDocumentService documentService,
            DocumentIntelligenceService docService)
        {
            _studentService = studentService;
            _applicationService = applicationService;
            _documentService = documentService;
            this._docExtractService = docService;
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

     [HttpPost("passport/extract")]
    public async Task<IActionResult> ExtractPassport([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        var fields = await _docExtractService.ExtractPassportDataAsync(stream);

        // Map known fields (optional enhancement)
        fields.TryGetValue("FirstName", out var firstName);
        fields.TryGetValue("LastName", out var lastName);
        fields.TryGetValue("DocumentNumber", out var passportNumber);
        fields.TryGetValue("DateOfIssue", out var issueDate);
        fields.TryGetValue("DateOfExpiration", out var expiryDate);
        fields.TryGetValue("CountryRegion", out var country);
        fields.TryGetValue("Nationality", out var nationality);



        return Ok(new
        {
            fullName = $"{firstName} {lastName}",
            passportNumber,
            passportIssueDate = issueDate,
            passportExpiryDate = expiryDate,
            country,
            nationality
        });
    }
    }