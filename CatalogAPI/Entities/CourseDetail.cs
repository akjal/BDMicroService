namespace CatalogAPI.Entities
{
    public class CourseDetails
    {
     public int Id { get; set; }


    public string InstructorName { get; set; } = null!;

    public int Credits { get; set; }

    public string Department { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public string CourseType { get; set; } = null!;

    public string? Prerequisites { get; set; }

    public string SemOrTermOffered { get; set; } = null!;

    public string Schedule { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int MaxEnrollment { get; set; }

    public int CurrentEnrollment { get; set; }

    public string CourseLevel { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string CourseFormat { get; set; } = null!;

    public string Fee { get; set; } = null!;

       // Foreign Key
    public int CourseId { get; set; }

    // Navigation Property
    public Course Course { get; set; }
    }
}