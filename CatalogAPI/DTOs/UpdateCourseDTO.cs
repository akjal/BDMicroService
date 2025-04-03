    namespace CatalogAPI.DTOs;
   
         public class UpdateCourseDTO
    {
        public Guid Id { get; set; }
       public string Name { get; set; }
        public decimal TuitionFee { get; set; }
        public string Duration { get; set; }
        public string IntakeMonths { get; set; }
        public int MaxEnrollment { get; set; }
        public int CurrentEnrollment {get; set; }
        public Guid UniversityId { get; set; }

    }
