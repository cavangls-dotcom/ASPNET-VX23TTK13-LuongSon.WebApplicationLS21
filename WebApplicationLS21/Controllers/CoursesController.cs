using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Models; // Đảm bảo import Models
using WebApplicationLS21.Data; // Đảm bảo import Data (Nếu CourseHubContext nằm ở Models, có thể không cần Data)

namespace WebApplicationLS21.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseHubContext _context;

        public CoursesController(CourseHubContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            // Tải danh sách khóa học, bao gồm thông tin Giảng viên (Instructor)
            var courseHubContext = _context.Courses.Include(c => c.Instructor);
            return View(await courseHubContext.ToListAsync());
        }

        // GET: Courses/Create
        // Hiển thị form để thêm khóa học mới
        public IActionResult Create()
        {
            // Truyền danh sách Giảng viên (Instructors) vào View để tạo dropdown list (Select List)
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FullName");
            return View();
        }

        // POST: Courses/Create
        // Xử lý dữ liệu khi người dùng gửi form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Description,Category,Price,DurationHours,InstructorID")] Course course)
        {
            // Loại bỏ lỗi ModelState cho các trường quan hệ (như Instructor)
            // if (!ModelState.IsValid) { ... } // Có thể bỏ qua kiểm tra này nếu bạn chưa dùng Data Annotations

            try
            {
                // Thêm khóa học vào Context
                _context.Add(course);

                // Lưu thay đổi vào Database
                await _context.SaveChangesAsync();

                // Chuyển hướng người dùng về trang Index (Danh sách khóa học)
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                // Ghi log lỗi nếu có vấn đề về Database
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator. Error: " + ex.Message);
            }

            // Nếu có lỗi, hiển thị lại form với danh sách Giảng viên (Instructors)
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "InstructorID", "FullName", course.InstructorID);
            return View(course);
        }

        // Lưu ý: Thêm các phương thức Details, Edit, Delete (GET/POST) dưới đây
        // ...
    }
}