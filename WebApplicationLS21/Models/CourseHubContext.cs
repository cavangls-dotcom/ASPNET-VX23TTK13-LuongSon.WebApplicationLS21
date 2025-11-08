using System.Collections;
using CourseHub.Models;
using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Data; // Nếu bạn dùng ApplicationDbContext.cs cho Users, hãy đảm bảo import này có
using WebApplicationLS21.Models;

namespace WebApplicationLS21.Models
{
    public class CourseHubContext : DbContext
    {
        // BẮT BUỘC: Constructor cho Dependency Injection
        public CourseHubContext(DbContextOptions<CourseHubContext> options)
            : base(options)
        {
        }

        // Khai báo các DbSet của bạn
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Instructor> Instructors { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Lesson> Lessons { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public IEnumerable Users { get; internal set; }

        // Giả sử bạn có model User trong dự án
        // public DbSet<User> Users { get; set; } = default!; 

        // Ghi đè phương thức này để cấu hình mô hình mối quan hệ
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BƯỚC QUAN TRỌNG: Vô hiệu hóa Delete Cascade cho tất cả các mối quan hệ
            // Điều này giải quyết lỗi "Multiple cascade paths" (Error 1785)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict; // Hoặc DeleteBehavior.NoAction
            }

            // Nếu bạn dùng ApplicationUser từ Identity, bạn có thể cần thêm cấu hình Identity ở đây.

            base.OnModelCreating(modelBuilder);
        }
    }
}