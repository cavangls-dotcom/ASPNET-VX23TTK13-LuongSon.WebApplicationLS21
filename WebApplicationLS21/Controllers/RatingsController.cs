using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Data;
using WebApplicationLS21.Models;

namespace WebApplicationLS21.Controllers
{
    public class RatingsController : Controller
    {
        private readonly CourseHubContext _context;

        public RatingsController(CourseHubContext context)
        {
            _context = context;
        }

        // -------------------------------------------
        // POST: Ratings/Create
        // -------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(int courseId, int rating, string? comment)
        {
            // ⚠ Kiểm tra Course tồn tại
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                return NotFound();

            // 👉 Giả sử UserID = 1 (bạn có thể thay bằng login user)
            int userId = 1;

            var review = new Review
            {
                CourseID = courseId,
                UserID = userId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // 👉 Quay lại trang chi tiết khóa học
            return RedirectToAction("Details", "Courses", new { id = courseId });
        }


        // -------------------------------------------
        // GET: Ratings/Delete/5
        // -------------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Course)
                .FirstOrDefaultAsync(m => m.ReviewID == id);

            if (review == null) return NotFound();

            return View(review);
        }

        // -------------------------------------------
        // POST: Ratings/DeleteConfirmed
        // -------------------------------------------
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            int courseId = review.CourseID;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Courses", new { id = courseId });
        }
    }
}
