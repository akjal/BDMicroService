
using CatalogAPI.Data;
using CatalogAPI.Entities;
using CatalogAPI.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CatalogAPI.Services.Implementations
{
    public class DocumentService : IDocumentService
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;

        public DocumentService(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

       public async Task UploadSingleDocumentAsync(Guid applicationId, IFormFile file, string documentType)
{
    if (file == null || file.Length == 0) return;

    var uploadPath = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads");
    if (!Directory.Exists(uploadPath))
        Directory.CreateDirectory(uploadPath);

    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
    var filePath = Path.Combine(uploadPath, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    var document = new Document
    {
        Id = Guid.NewGuid(),
        ApplicationId = applicationId,
        DocumentType = documentType,
        FilePath = Path.Combine("uploads", fileName), // Relative path
        UploadDate = DateTime.UtcNow
    };

    _context.Documents.Add(document);
    await _context.SaveChangesAsync();
}

    }
}
