    namespace CatalogAPI.DTOs;
    
    public class ApplicationSubmissionDTO
    {
        public string StudentJson { get; set; }
        public string ApplicationJson { get; set; }

        public IFormFile Passport { get; set; }
        public IFormFile Transcript { get; set; }
        
    }
