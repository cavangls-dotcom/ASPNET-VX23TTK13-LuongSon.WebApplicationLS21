using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Models;
using System.Linq; // Cần cho OnModelCreating

namespace WebApplicationLS21.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Đây là constructor chuẩn duy nhất cần thiết cho Dependency Injection (DI) 
        // và các công cụ EF Core (khi sử dụng Factory).
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ... Các DbSet properties của bạn
        public DbSet<Course> Courses { get; set; }
        public object Instructors { get; internal set; }

        // ... (Các DbSet khác như Enrollments, Students, Instructors)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Code sửa lỗi cascade paths của bạn
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}