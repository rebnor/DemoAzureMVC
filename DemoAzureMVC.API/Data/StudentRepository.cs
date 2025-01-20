using DemoAzureMVC.Shared;
using Microsoft.EntityFrameworkCore;

namespace DemoAzureMVC.API.Data
{
    public class StudentRepository : IStudent
    {
        private readonly ApplicationDbContext appDbCtx;
        private readonly ILogger<StudentRepository> logger;

        public StudentRepository(ApplicationDbContext appDbCtx, ILogger<StudentRepository> logger)
        {
            this.appDbCtx = appDbCtx;
            this.logger = logger;
        }

        public async Task<Student?> AddStudentAsync(Student student)
        {
            try
            {
                if (student == null)
                {
                    logger.LogWarning("Attempted to add a null student.");
                    throw new ArgumentNullException(nameof(student), "Student cannot be null");
                }
                appDbCtx.Students.Add(student);
                await appDbCtx.SaveChangesAsync();
                return student;
            }
            catch (ArgumentNullException ex)
            {
                logger.LogWarning(ex, "Failed to add student: Null student argument.");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while adding student, message: {ex.Message}");
                throw new Exception($"An error occurred while adding student, message: {ex.Message}");
            }
        }

        public async Task DeleteStudentAsync(Student student)
        {
            try
            {
                if (student == null)
                {
                    logger.LogWarning("Attempted to delete a null student.");
                    throw new ArgumentNullException(nameof(student), "Student cannot be null");
                }
                appDbCtx.Students.Remove(student);
                await appDbCtx.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                logger.LogWarning(ex, "Failed to delete student: Null student argument.");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting student, message: {ex.Message}");
                throw new Exception($"An error occurred while adding student, message: {ex.Message}");
            }
        }

        public async Task<List<Student>?> GetAllStudentsAsync()
        {
            try
            {
                return await appDbCtx.Students.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while fetching all the students, message: {ex.Message}");
                throw new Exception($"An error occurred while fetching all the students, message: {ex.Message}");
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            try
            {
                var student = await appDbCtx.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student == null)
                {
                    logger.LogWarning($"No student with id '{id}' found.");
                    throw new ArgumentNullException(nameof(id), $"No student with id '{id}' found.");
                }
                return student;
            }
            catch (ArgumentNullException ex)
            {
                logger.LogWarning(ex, $"Failed to find student: No student with id '{id}'.");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while fetching student by id '{id}' , message: {ex.Message}");
                throw new Exception($"An error occurred while fetching student by id '{id}' , message: {ex.Message}");
            }
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var existingStudent = await appDbCtx.Students.FindAsync(student.Id);
            if (existingStudent == null)
            {
                return null;
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.PictureUrl = student.PictureUrl;
            existingStudent.IsWizard = student.IsWizard;

            appDbCtx.Students.Update(existingStudent);
            await appDbCtx.SaveChangesAsync();

            return existingStudent;
        }


    }
}
