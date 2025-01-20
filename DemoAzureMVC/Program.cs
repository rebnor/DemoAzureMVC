using DemoAzureMVC.Data;

namespace DemoAzureMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // HttpClient-config to docker/API
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://demoazuremvcapi-app-202501071801.livelyocean-dfa16904.northeurope.azurecontainerapps.io") });
            //HttpClient-config to Localhost
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5293") });


            builder.Services.AddTransient<IStudent, StudentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHttpsRedirection();
                app.UseHsts(); 
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
