namespace CatalogAPI.Entities;

public  class UniversityDetail
{    public int Id { get; set; }

    public int? UniversityId { get; set; }

    public string? UniversityType { get; set; }

    public string? Address { get; set; }

    public int? EstablishedYear { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public int? NumberofStudents { get; set; }

    public int? NumberofStaff { get; set; }

    public string? AccreditationStatus { get; set; }

    public string? ProgramsOffered { get; set; }

    public int? UniversityRanking { get; set; }

    public string? UniversityLogo { get; set; }

    public string? CampusSize { get; set; }

    public string? StudentToFacultyRatio { get; set; }

    public string? ResearchFacilities { get; set; }

    public string? Endowment { get; set; }

    public string? LanguageOfInstruction { get; set; }
}
