using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseHub.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace WebApplicationLS21.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string? FullName { get; set; }

        [EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [Required, StringLength(20)]
        public string Role { get; set; } = "Student"; // "Admin", "Instructor", "Student"

        // Quan hệ với các bảng khác
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
