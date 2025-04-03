using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace CatalogAPI.Entities;

public class Course
{
    public Guid Id { get; set; }  // This is the Course primary key
    [MaxLength(128)]

    public required string Name { get; set; }
    [MaxLength(250)]

    public string? Description { get; set; }
    [MaxLength(128)]

    public string? Duration { get; set; }
    [MaxLength(128)]

    public string? IntakeMonths { get; set; }
    [Precision(10, 2)]

    public decimal TuitionFee { get; set; }
    public int MaxEnrollment { get; set; }
    public int CurrentEnrollment { get; set; }


    public Guid UniversityId { get; set; }     // Foreign key
    public University University { get; set; } // Navigation property
     public ICollection<Application> Applications { get; set; }
}
