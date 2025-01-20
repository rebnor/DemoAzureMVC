using DemoAzureMVC.Shared;

namespace DemoAzureMVC.API.Data
{
    public interface IStudent
    {
        Task<Student?> GetStudentByIdAsync(int id);
        Task<List<Student>?> GetAllStudentsAsync();
        Task<Student?> AddStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
    }
}
