// Đảm bảo bạn sử dụng đúng DbContext của mình, ví dụ: CourseHubContext
using WebApplicationLS21.Models;
using System.Linq;

namespace WebApplicationLS21.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CourseHubContext context)
        {
            // Đảm bảo Database đã được tạo. 
            // KHÔNG dùng EnsureCreated() nếu bạn dùng Migration.

            // Nếu đã có dữ liệu Giảng viên, không cần Seed lại
            if (context.Instructors.Any())
            {
                return;   // Database đã được Seed
            }

            // 1. Tạo Giảng viên (Instructors)
            var instructors = new Instructor[]
            {
                new Instructor { FullName = "TS. Đoàn Phước Miền", Email = "antonio86doan@gmail.com" },
                new Instructor { FullName = "Trần Thị B", Email = "ttb@coursehub.edu" },
                // Thêm giảng viên khác nếu cần
            };
            // Sử dụng AddRange trên DbSet<Instructor>
            context.Instructors.AddRange(instructors);
            context.SaveChanges(); // Lưu ngay sau khi thêm Instructor để lấy ID

            // 2. Tạo Khóa học (Courses)
            var courses = new Course[]
            {
                new Course {
                    Title = "Lập trình C# cơ bản",
                    Category = "Lập trình",
                    Price = 500000,
                    DurationHours = 40,
                    InstructorID = instructors.Single(i => i.FullName == "Nguyễn Văn A").InstructorID
                },
                new Course {
                    Title = "Thiết kế UI/UX với Figma",
                    Category = "Thiết kế",
                    Price = 350000,
                    DurationHours = 30,
                    InstructorID = instructors.Single(i => i.FullName == "Trần Thị B").InstructorID
                },
                // Thêm các khóa học khác
            };
            // Sử dụng AddRange trên DbSet<Course>
            context.Courses.AddRange(courses);

            // THIẾT LẬP DỮ LIỆU cho các bảng khác (Enrollment, Lesson, Review) tương tự ở đây...

            // Lưu tất cả thay đổi còn lại
            context.SaveChanges();
        }
    }
}