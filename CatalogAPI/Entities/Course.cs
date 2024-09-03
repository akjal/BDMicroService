namespace CatalogAPI.Entities;

public class Course
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Duration {get;set;}
    public string? Type {get;set;}
    public DateTime CreatedTimestamp { get; set; }

}

