using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseHub.Models;

namespace WebApplicationLS21.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Category { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        // FK
        public int? InstructorID { get; set; }
        public Instructor? Instructor { get; set; }

        public int DurationHours { get; set; }

        // Only 1 file property
        public string? ContentFilePath { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
