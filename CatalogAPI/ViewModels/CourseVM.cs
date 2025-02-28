namespace CatalogAPI.ViewModels;
public class CourseVM

{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Duration { get; set; }
    public string? Type { get; set; } 
     public string? Description { get; set; }
    public int MaxEnrollment { get; set; }
    public int CurrentEnrollment { get; set; }

}