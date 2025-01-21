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
        private readonly ILogger<StudentController> logger;

        public StudentController(IStudent studentRepo, ILogger<StudentController> logger)
        {
            this.studentRepo = studentRepo;
            this.logger = logger;
        }

        // GET: Student/Index
        public async Task<ActionResult> Index()
        {
            try
            {
                var students = await studentRepo.GetAllStudentsAsync();
                if (students != null)
                {
                    logger.LogInformation($"--> Fetched {students.Count} students.");
                    return View(students);
                }
                else
                {
                    logger.LogInformation("--> Empty students-list fetched.");
                    return View(new List<Student>());
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"--> Error occurred while fetching all students: {ex.Message}");
                return View(new List<Student>());
            }
        }

        // GET: Student/Details/{id}
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var student = await studentRepo.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"--> Error occurred while fetching student by id {id}, message: {ex.Message}");
                return RedirectToAction(nameof(Error), "Student", new { message = ex.Message });
            }
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new student */
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                await studentRepo.CreateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogInformation($"--> Error occurred while creating student: {ex.Message}");
                return RedirectToAction(nameof(Error), "Student", new { message = ex.Message });
            }
        }

        // GET: Student/Delete/{id}
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var student = await studentRepo.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"--> Error occurred while fetching student: {ex.Message}");
                return RedirectToAction(nameof(Error), "Student", new { message = ex.Message });
            }
        }

        // POST: Delete student 
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
                logger.LogInformation($"--> Error occurred while deleting student: {ex.Message}");
                return RedirectToAction(nameof(Error), "Student", new { message = ex.Message });
            }
        }

        // GET: Student/Update/{id} 
        public async Task<ActionResult> Update(int id) 
        {
            try
            {
                var student = await studentRepo.GetStudentByIdAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"--> Error occurred while updating student: {ex.Message}");
                return RedirectToAction(nameof(Error), "Student", new { message = ex.Message });
            }
        }

        // PUT: Update student 
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
                logger.LogInformation($"--> Error occurred while updating student: {ex.Message}");
                return RedirectToAction(nameof(Error), "Student", new { message = ex.Message });
            }
        }

        // Redirect to Error View with error message 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = message });
        }
    }
}
