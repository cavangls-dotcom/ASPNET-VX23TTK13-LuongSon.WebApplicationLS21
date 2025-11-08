using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Data; // Đảm bảo đã có
using WebApplicationLS21.Models;
using Microsoft.Extensions.DependencyInjection; // Cần thiết cho GetRequiredService

var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ✅ Cấu hình kết nối Database (EF Core)
// Chú ý: Bạn đang dùng CourseHubContext. Nếu bạn muốn dùng ApplicationDbContext, hãy thay đổi tên Context ở đây.
builder.Services.AddDbContext<CourseHubContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- BẮT ĐẦU: KHỐI THỰC THI SEED DATA ---
// Tạo scope mới để lấy DbContext service
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Sử dụng CourseHubContext để lấy dịch vụ. Đảm bảo Context này đã được đăng ký.
        var context = services.GetRequiredService<CourseHubContext>();

        // Thực thi hàm khởi tạo dữ liệu
        // Nếu bạn đã tạo DbInitializer.cs, hãy chắc chắn nó dùng đúng tên Context
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}
// --- KẾT THÚC: KHỐI THỰC THI SEED DATA ---


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();