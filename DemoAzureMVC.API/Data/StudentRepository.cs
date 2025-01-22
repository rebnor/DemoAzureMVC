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
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null");
            }
            appDbCtx.Students.Add(student);
            await appDbCtx.SaveChangesAsync();
            logger.LogInformation("Added student with ID {Id}", student.Id);
            return student;
        }

        public async Task DeleteStudentAsync(Student student)
        {
            if (student == null)
            {
                logger.LogWarning("Attempted to delete a null student.");
                throw new ArgumentNullException(nameof(student), "Student cannot be null");
            }
            appDbCtx.Students.Remove(student);
            await appDbCtx.SaveChangesAsync();
        }

        public async Task<List<Student>?> GetAllStudentsAsync()
        {
            return await appDbCtx.Students.ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            var student = await appDbCtx.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                logger.LogWarning($"No student with id '{id}' found.");
                throw new KeyNotFoundException($"No student with id '{id}' found.");

            }
            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var existingStudent = await appDbCtx.Students.FindAsync(student.Id);
            if (existingStudent == null)
            {
                throw new KeyNotFoundException($"Student with ID {student.Id} not found.");
            }
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.PictureUrl = student.PictureUrl;
            existingStudent.IsWizard = student.IsWizard;
            appDbCtx.Students.Update(existingStudent);
            await appDbCtx.SaveChangesAsync();
            logger.LogInformation("Updated student with ID {Id}", student.Id);
            return existingStudent;
        }
    }
}
