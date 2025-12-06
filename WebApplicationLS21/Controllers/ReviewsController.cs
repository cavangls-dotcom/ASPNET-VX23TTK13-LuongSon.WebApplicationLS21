using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Data;
using WebApplicationLS21.Models;

namespace WebApplicationLS21.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly CourseHubContext _context;

        public ReviewsController(CourseHubContext context)
        {
            _context = context;
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CourseID, int Rating, string? Comment)
        {
            // ⚠ TẠM THỜI gán UserID = 1 (sau này thay bằng đăng nhập)
            var review = new Review
            {
                CourseID = CourseID,
                UserID = 1,
                Rating = Rating,
                Comment = Comment,
                CreatedAt = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }

            // Quay lại trang chi tiết khóa học
            return RedirectToAction("Details", "Courses", new { id = CourseID });
        }
    }
}
