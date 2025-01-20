using DemoAzureMVC.Shared;


namespace DemoAzureMVC.Data
{
    public interface IStudent
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<Student> UpdateStudentAsync(Student student);
    }
}
