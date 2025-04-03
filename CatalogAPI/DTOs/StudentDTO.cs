
namespace CatalogAPI.DTOs;
public class StudentDTO
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string? PassportNumber { get; set; }   
    public DateTime PassportIssueDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public string? ContactNumber { get; set; }
    public string? Address { get; set; }
   


}
