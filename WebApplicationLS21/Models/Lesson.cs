using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationLS21.Models;

namespace CourseHub.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? VideoUrl { get; set; }
        public string? Content { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        public Course? Course { get; set; }
    }
}
