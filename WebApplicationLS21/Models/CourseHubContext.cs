using CourseHub.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationLS21.Models
{
    public class CourseHubContext : DbContext
    {
        public CourseHubContext(DbContextOptions<CourseHubContext> options)
            : base(options)
        {
        }

        // Các DbSet đại diện cho các bảng trong CSDL
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
