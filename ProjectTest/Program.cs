public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Cấu hình Session
        builder.Services.AddDistributedMemoryCache(); // Cần dùng MemoryCache để lưu trữ Session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
            options.Cookie.HttpOnly = true; // Cookie chỉ có thể được truy cập từ máy chủ
            options.Cookie.IsEssential = true; // Cookie cần thiết cho việc lưu trữ
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseSession();
        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
