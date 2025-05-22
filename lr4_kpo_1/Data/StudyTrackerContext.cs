using lr4_kpo_1.Models;
using Microsoft.EntityFrameworkCore;
using Task = lr4_kpo_1.Models.Task;

namespace lr4_kpo_1.Data
{
    public class StudyTrackerContext : DbContext
    {
        public StudyTrackerContext(DbContextOptions<StudyTrackerContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CourseId);

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Math", Description = "Some description", ProfessorName = "Ivanov" },
                new Course { Id = 2, Name = "History", Description = "World history", ProfessorName = "Petrov" },
                new Course { Id = 3, Name = "Computer Science", Description = "Introduction to programming", ProfessorName = "Sidorov" }
            );
        }
    }
}