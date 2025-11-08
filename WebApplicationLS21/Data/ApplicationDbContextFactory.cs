using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApplicationLS21.Models;

namespace WebApplicationLS21.Data
{
    // Class này được sử dụng bởi Entity Framework Core CLI (Update-Database) 
    // để tìm và khởi tạo DbContext một cách rõ ràng, giải quyết lỗi Design-time.
    public class CourseHubContextFactory : IDesignTimeDbContextFactory<CourseHubContext>
    {
        public CourseHubContext CreateDbContext(string[] args)
        {
            // 1. Tạo đối tượng Configuration để đọc appsettings.json
            // Đây là cách duy nhất để EF Core CLI biết đường dẫn.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Đặt base path là thư mục chạy lệnh
                .AddJsonFile("appsettings.json")
                .Build();

            // 2. Lấy chuỗi kết nối
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // 3. Xây dựng Options
            var builder = new DbContextOptionsBuilder<CourseHubContext>();
            builder.UseSqlServer(connectionString);

            // 4. Trả về Context đã được khởi tạo
            return new CourseHubContext(builder.Options);
        }
    }
}