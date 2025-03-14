using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
   public DbSet<Course> Courses { get; set; }
   public DbSet<CourseDetails> CourseDetails { get; set; }

   public DbSet<University> Universities { get; set; }
    public DbSet<Application> Applications { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasOne(c => c.CourseDetails)
            .WithOne(cd => cd.Course)
            .HasForeignKey<CourseDetails>(cd => cd.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
