using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Models;
using System.Linq; // Cần cho OnModelCreating

namespace WebApplicationLS21.Data
{
    public class ApplicationDbContext : DbContext
    {
               public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

                public DbSet<Course> Courses { get; set; }
        public object Instructors { get; internal set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                       foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}