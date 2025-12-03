using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationLS21.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorID { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public string? Email { get; set; }
        public string? Bio { get; set; }

        // Quan hệ 1 - Nhiều với Course
        public ICollection<Course>? Courses { get; set; }
    }
}
