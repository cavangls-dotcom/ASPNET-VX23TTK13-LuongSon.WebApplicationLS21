using System;
using System.ComponentModel.DataAnnotations;
using WebApplicationLS21.Models;

namespace WebApplicationLS21.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        // KHÔNG DÙNG ForeignKey attribute ở đây
        public int UserID { get; set; }
        public int CourseID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}
