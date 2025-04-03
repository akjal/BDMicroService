namespace CatalogAPI.DTOs;

public class ApplicationDTO
{
         public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }

        public Guid CourseId { get; set; }
        public string CourseName { get; set; }

        public Guid UniversityId { get; set; }
        public string UniversityName { get; set; }

        public string ApplicationStatus { get; set; }
        public DateTime CreatedOn { get; set; }

}
