using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
   public DbSet<Course> Courses { get; set; }
   public DbSet<CourseDetail> CourseDetails { get; set; }

   public DbSet<University> Universities { get; set; }

   

}
