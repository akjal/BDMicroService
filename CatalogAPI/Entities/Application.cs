using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CatalogAPI.Entities;


public class Application
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string? ApplicationStatus { get; set; } // e.g., Submitted, Reviewed, Offered, Rejected

    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public Offer Offer { get; set; }
    public ICollection<Document> Documents { get; set; }
    // public CASLetter CASLetter { get; set; }
     public ICollection<Payment> Payments { get; set; }

}
