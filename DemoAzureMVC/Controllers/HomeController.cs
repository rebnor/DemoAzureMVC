using System.Diagnostics;
using DemoAzureMVC.Data;
using DemoAzureMVC.Models;
using Microsoft.AspNetCore.Mvc;
using DemoAzureMVC.Shared;


namespace DemoAzureMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudent studentRepo;

        public HomeController(ILogger<HomeController> logger, IStudent studentRepo)
        {
            _logger = logger;
            this.studentRepo = studentRepo;
        }

        public async Task<ActionResult> Index()
        {
            //var student = await studentRepo.GeStudentByIdAsync(1);
            //return View(student);
            return View();
            //var students = await studentRepo.GetAllStudentsAsync();
            //return View(students);
            //try
            //{
            //    var students = await studentRepo.GetAllStudentsAsync();
            //    if (students != null)
            //    {
            //        Console.WriteLine($"Fetched {students.Count} students.");
            //        return View(students);
            //    }
            //    else
            //    {
            //        Console.WriteLine("No students fetched.");
            //        return View(new List<Student>()); // Returnerar tom lista om det misslyckas
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //    return View(new List<Student>());
            //}
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
