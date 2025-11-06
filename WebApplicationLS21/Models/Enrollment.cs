using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationLS21.Models;

namespace CourseHub.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        public DateTime EnrolledAt { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}
