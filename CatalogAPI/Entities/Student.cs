using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CatalogAPI.Entities;
public class Student
{
    public Guid Id { get; set; }
    [MaxLength(128)]    
    public required string FirstName { get; set; }
    [MaxLength(128)]
    public string? LastName { get; set; }
    [MaxLength(128)]
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    [MaxLength(128)]
    public string? Nationality { get; set; }
    [MaxLength(128)]
    public string? PassportNumber { get; set; }   
    public DateTime PassportIssueDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    [MaxLength(50)]
    public string? ContactNumber { get; set; }
    [MaxLength(500)]
    public string? Address { get; set; }
    public ICollection<Application> Applications { get; set; }
    public ICollection<Payment> Payments { get; set; }


}
