public class Application
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
    public DateTime PassportIssueDate { get; set; }
    public DateTime PassportExpiryDate { get; set; }
    public int UniversityId { get; set; }
    public string Course { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
