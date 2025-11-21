using CourseHub.Models;
using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Models; // Đảm bảo bạn có Model Course, Instructor, v.v.

namespace WebApplicationLS21.Data
{
    // Đảm bảo class này kế thừa từ DbContext
    public class CourseHubContext : DbContext
    {
        public CourseHubContext(DbContextOptions<CourseHubContext> options)
            : base(options)
        {
        }

        // Khai báo các DbSet
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Instructor> Instructors { get; set; } = default!;
        // Thêm các DbSet khác: Enrollment, Lesson, Review...
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Lesson> Lessons { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SỬA LỖI CẢNH BÁO: Cấu hình độ chính xác cho thuộc tính Price
            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                // Đặt Precision là 18 (tổng số chữ số) và Scale là 2 (số chữ số sau dấu thập phân)
                .HasPrecision(18, 2);

            // Đảm bảo tên bảng là số nhiều, nếu bạn muốn tuân theo quy ước chung
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Lesson>().ToTable("Lesson");
            modelBuilder.Entity<Review>().ToTable("Review");

            // 3. KHẮC PHỤC LỖI MULTIPLE CASCADE PATHS (SQL Server Error 1785)
            // Lỗi này xảy ra khi có nhiều đường dẫn xóa phân cấp trong mối quan hệ.
            // Chúng ta vô hiệu hóa hành vi ON DELETE CASCADE mặc định cho tất cả các khóa ngoại.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                // Nếu hành vi xóa là Cascade (mặc định của EF Core), ta đặt lại thành Restrict.
                if (relationship.DeleteBehavior == DeleteBehavior.Cascade)
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}