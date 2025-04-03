using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CatalogAPI.Entities;
public class Document
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; }
     [MaxLength(50)]

    public string? DocumentType { get; set; } // e.g., Transcript, SOP, Passport
    [MaxLength(500)]

    public string? FilePath { get; set; }     // Path/URL to document
    public DateTime UploadDate { get; set; }
}
