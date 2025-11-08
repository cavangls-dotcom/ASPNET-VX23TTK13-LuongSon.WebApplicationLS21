using WebApplicationLS21.Models;
using System.Linq;
using WebApplicationLS21.Data; // Giả sử DbContext của bạn nằm đây
using Microsoft.EntityFrameworkCore; // Cần thiết cho các loại DbContext

namespace WebApplicationLS21.Data
{
    // Đảm bảo bạn sử dụng đúng tên Context, ví dụ: CourseHubContext
    public static class DbInitializer
    {
        public static void Initialize(CourseHubContext context) // Sửa tên Context nếu cần
        {
            context.Database.EnsureCreated();

            // 1. Kiểm tra nếu đã có khóa học, thoát nếu có
            if (context.Courses.Any())
            {
                return;   // DB đã được seed
            }

            // --- 2. Thêm Giảng viên Mẫu (Instructor/User) ---
            var instructors = new Instructor[]
            {
                new Instructor { FullName = "Nguyễn Văn A", Email = "nguyen.a@test.com" },
                new Instructor { FullName = "Trần Thị B", Email = "tran.b@test.com" }
            };

            // Dùng AddRange để thêm cả mảng
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            // --- 3. Thêm Khóa học Mẫu (Courses) ---
            var courses = new Course[]
            {
                new Course
                {
                    Title = "Lập trình C# cơ bản",
                    Category = "Lập trình",
                    Price = 99.99M,
                    Description = "Giới thiệu về ngôn ngữ C# và .NET",
                    InstructorID = instructors.Single(i => i.FullName == "Nguyễn Văn A").InstructorID,
                    ImageUrl = "/images/csharp.png"
                },
                new Course
                {
                    Title = "Thiết kế Web với ASP.NET Core",
                    Category = "Lập trình Web",
                    Price = 149.50M,
                    Description = "Xây dựng ứng dụng web hiện đại",
                    InstructorID = instructors.Single(i => i.FullName == "Trần Thị B").InstructorID,
                    ImageUrl = "/images/aspnet.png"
                },
            };

            // Dùng AddRange để thêm cả mảng
            context.Courses.AddRange(courses);
            context.SaveChanges();
        }
    }
}