// using CatalogAPI.Data;
// using CatalogAPI.DTOs;
// using CatalogAPI.Entities;
// using CatalogAPI.DTOs;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace CatalogAPI.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class ApplicationsController(DataContext context,DocumentIntelligenceService docService) : ControllerBase
// {
// [HttpPost("passport/extractsample")]
// public async Task<IActionResult> ExtractPassportInfo(IFormFile file)
// {
//     // Run OCR logic (e.g., Tesseract OCR or Azure Form Recognizer)
//     var extractedData = new
//     {
//         FullName = "John Doe",
//         Email = "john@example.com",
//         PassportNumber = "M1234567",
//         PassportIssueDate = "2023-01-01",
//         PassportExpiryDate = "2033-01-01"
//     };

//     return Ok(extractedData);
// }

// [HttpPost("passport/extract")]
//     public async Task<IActionResult> ExtractPassport([FromForm] IFormFile file)
//     {
//         if (file == null || file.Length == 0)
//             return BadRequest("No file uploaded.");

//         using var stream = file.OpenReadStream();
//         var fields = await docService.ExtractPassportDataAsync(stream);

//         // Map known fields (optional enhancement)
//         fields.TryGetValue("FirstName", out var firstName);
//         fields.TryGetValue("LastName", out var lastName);
//         fields.TryGetValue("DocumentNumber", out var passportNumber);
//         fields.TryGetValue("DateOfIssue", out var issueDate);
//         fields.TryGetValue("DateOfExpiration", out var expiryDate);
//         fields.TryGetValue("CountryRegion", out var country);
//         fields.TryGetValue("Nationality", out var nationality);



//         return Ok(new
//         {
//             fullName = $"{firstName} {lastName}",
//             passportNumber,
//             passportIssueDate = issueDate,
//             passportExpiryDate = expiryDate,
//             country,
//             nationality
//         });
//     }
// [HttpPost]
//     public async Task<IActionResult> SubmitApplication([FromBody] ApplicationDto dto)
//     {
//         if (!ModelState.IsValid) return BadRequest(ModelState);

//         var application = new Application
//         {
//             FullName = $"{dto.FirstName} {dto.LastName}",
//             Email = dto.Sttudent.Email,
//             PassportNumber = dto.PassportNumber,
//             PassportIssueDate = dto.PassportIssueDate,
//             PassportExpiryDate = dto.PassportExpiryDate,
//             UniversityId = dto.UniversityId,
//             Course = dto.Course
//         };

//         context.Applications.Add(application);
//         await context.SaveChangesAsync();

//         return Ok(new { message = "Application submitted successfully", applicationId = application.Id });
//     }


// }