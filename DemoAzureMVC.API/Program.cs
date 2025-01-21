
using DemoAzureMVC.API.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoAzureMVC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AzureStudentsDb")
                    //sqlOptions => sqlOptions.EnableRetryOnFailure(
                    //    maxRetryCount: 3,
                    //    maxRetryDelay: TimeSpan.FromSeconds(10),
                    //    errorNumbersToAdd: null))
            ));

            builder.Services.AddControllers();
         
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<IStudent, StudentRepository>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMVC", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:5188",
                        "https://localhost:7038",
                        "https://demoazuremvc20250107111941.azurewebsites.net"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowMVC");
            app.MapControllers();

            app.Run();
        }
    }
}
