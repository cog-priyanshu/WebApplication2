
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = "Server=DESKTOP-F1FDR28\\SQLEXPRESS;Database=hy;Trusted_Connection=True;TrustServerCertificate=True;";

            // Fix: EnableSensitiveDataLogging and EnableDetailedErrors are methods of DbContextOptionsBuilder, not IServiceCollection.  
            builder.Services.AddDbContext<AppDBContext>(options =>
            {
                options.UseSqlServer(connectionString)
                       .EnableSensitiveDataLogging()
                       .EnableDetailedErrors();
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRazorPages();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Uncomment Swagger setup if needed  
                // app.UseSwagger();  
                // app.UseSwaggerUI();  
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
