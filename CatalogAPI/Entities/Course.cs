namespace CatalogAPI.Entities;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public string? Description { get; set; }
    // Navigation Property
   public CourseDetails CourseDetails { get; set; }
}

