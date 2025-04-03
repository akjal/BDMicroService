using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogAPI.Entities;
public class University
{
    public Guid Id { get; set; }
    [MaxLength(128)]

    public  required string Name { get; set; }
    [MaxLength(128)]

    public string? Website { get; set; }
    [MaxLength(128)]

    public string? Country { get; set; }
    [MaxLength(10)]

    public string? City { get; set; }
     [MaxLength(128)]

    public string? ContactEmail { get; set; }

    
    public ICollection<Course> Courses { get; set; }

   
  
}
