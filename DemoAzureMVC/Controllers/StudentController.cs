using DemoAzureMVC.Data;
using DemoAzureMVC.Models;
using DemoAzureMVC.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoAzureMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent studentRepo;

        public StudentController(IStudent studentRepo)
        {
            this.studentRepo = studentRepo;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var students = await studentRepo.GetAllStudentsAsync();
                if (students != null)
                {
                    Console.WriteLine($"--> Fetched {students.Count} students.");
                    return View(students);
                }
                else
                {
                    Console.WriteLine("--> No students fetched.");
                    return View(new List<Student>()); // Returnerar tom lista om det misslyckas
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error: {ex.Message}");
                return View(new List<Student>());
            }
        }

        //GET: View: Controller/Details/id
        public async Task<ActionResult> Details(int id)
        {
            var student = await studentRepo.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        /* GET: View: Controller/Create */
        public IActionResult Create()
        {
            return View();
        }
        /* POST: Create new student */
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            await studentRepo.CreateStudentAsync(student);
            return RedirectToAction(nameof(Index));
        }

        /* GET: View: Controller/Delete/id */
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var student = await studentRepo.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        /* POST: Delete student */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await studentRepo.DeleteStudentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting student: {ex.Message}");
                return View("Error");
            }
        }

        /* GET: View: Controller/Update/id */
        public async Task<ActionResult> Update(int id) 
        {
            var student = await studentRepo.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        /* PUT: Update student */
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateConfirmed(int id, [Bind("Id,FirstName,LastName,PictureUrl,IsWizard")] Student student)
        {
            try
            {
                var exsistingStudent = await studentRepo.GetStudentByIdAsync(id);
                if (exsistingStudent.Id == student.Id)
                {
                    var updatedStudent = await studentRepo.UpdateStudentAsync(student);
                    return RedirectToAction(nameof(Details), new { id = updatedStudent.Id });
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error occurred while updating student: {ex.Message}");
                //return View("Error");
                ModelState.AddModelError(string.Empty, "An error occurred while updating the student.");
                return View(student);
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
