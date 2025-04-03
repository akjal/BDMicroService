namespace CatalogAPI.DTOs;
    public class UpdateApplicationDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string ApplicationStatus { get; set; }
    }
