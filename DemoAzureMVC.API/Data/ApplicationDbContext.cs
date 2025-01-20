using DemoAzureMVC.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DemoAzureMVC.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<Student> Students { get; set; }

    }
}
