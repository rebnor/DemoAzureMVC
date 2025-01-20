using DemoAzureMVC.API.Data;
using DemoAzureMVC.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoAzureMVC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> logger;
        private readonly IStudent studentRepo;

        public StudentController(ILogger<StudentController> logger, IStudent studentRepo)
        {
            this.logger = logger;
            this.studentRepo = studentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            try
            {
                var students = await studentRepo.GetAllStudentsAsync();
                if (students == null)
                {
                    logger.LogInformation("No students found.");
                    return Ok(students);
                }
                return Ok(students);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting students, message: {ex.Message}");

            }
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<List<Student>>> GetStudentById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    logger.LogWarning("Invalid student ID requested.");
                    return BadRequest("Invalid student ID.");
                }

                var student = await studentRepo.GetStudentByIdAsync(id);
                if (student == null)
                {
                    logger.LogWarning($"Student with ID {id} not found.");
                    return NotFound();
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting student by id, message: {ex.Message}");

            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            try
            {
                if (student == null)
                {
                    logger.LogWarning("Attempted to add a null student.");
                    return BadRequest("Student cannot be null.");
                }
                var addedStudent = await studentRepo.AddStudentAsync(student);
                logger.LogInformation($"Student added successfully with ID: {addedStudent.Id}");
                return CreatedAtAction(nameof(GetStudents), new { id = addedStudent.Id }, addedStudent);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogWarning(ex, "Attempted to add a null student.");
                return BadRequest("Student cannot be null.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding student, message: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                if (id <= 0)
                {
                    logger.LogWarning("Invalid student ID requested.");
                    return BadRequest("Invalid student ID.");
                }
                var student = await studentRepo.GetStudentByIdAsync(id);
                if (student == null)
                {
                    logger.LogWarning($"Student with ID {id} not found.");
                    return NotFound();
                }
                await studentRepo.DeleteStudentAsync(student);
                logger.LogInformation($"Student with ID {id} deleted successfully.");
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting the student.");
                return StatusCode(500, $"An error occurred while deleting the student, message: {ex.Message}");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
        {
            try
            {
                if (id <= 0)
                {
                    logger.LogWarning("Invalid student ID requested.");
                    return BadRequest("Invalid student ID.");
                }

                if (student == null || id != student.Id)
                {
                    logger.LogWarning("Mismatched or invalid student object.");
                    return BadRequest("Invalid student data.");
                }

                var updatedStudent = await studentRepo.UpdateStudentAsync(student);
                if (updatedStudent == null)
                {
                    logger.LogWarning($"Student with ID {id} not found.");
                    return NotFound($"Student with ID {id} not found.");
                }

                return Ok(updatedStudent);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating the student.");
                return StatusCode(500, $"An error occurred while updating the student, message: {ex.Message}");
            }
        }



    }
}
