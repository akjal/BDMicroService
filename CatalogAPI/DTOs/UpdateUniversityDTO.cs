namespace CatalogAPI.DTOs;
public class UpdateUniversityDTO
    {
        public Guid Id { get; set; }
         public required string Name { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public string? ContactEmail { get; set; }
    }
