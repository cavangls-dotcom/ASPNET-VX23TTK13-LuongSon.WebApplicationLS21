using Microsoft.EntityFrameworkCore;
using WebApplicationLS21.Models; // ✅ dùng namespace chứa CourseHubContext

var builder = WebApplication.CreateBuilder(args);

// ✅ Cấu hình kết nối đến CourseHubDB
builder.Services.AddDbContext<CourseHubContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
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
