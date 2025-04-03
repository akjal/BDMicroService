namespace CatalogAPI.DTOs;

public class CreateApplicationDTO
{       public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string ApplicationStatus { get; set; }
        
}   