using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Student - Application (One-to-Many)
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Student)
                .WithMany(s => s.Applications)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // University - Course (One-to-Many)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.University)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course - Application (One-to-Many)
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Applications)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Application - Offer (One-to-One)
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Application)
                .WithOne(a => a.Offer)
                .HasForeignKey<Offer>(o => o.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Application - Documents (One-to-Many)
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Application)
                .WithMany(a => a.Documents)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

          
            // Student - Payments (One-to-Many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Student)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.StudentId)
                 .OnDelete(DeleteBehavior.Restrict); //

            // Application - Payments (One-to-Many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Application)
                .WithMany(a => a.Payments)
                .HasForeignKey(p => p.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }

}
