using DemoAzureMVC.Shared;

namespace DemoAzureMVC.Data
{
    public class StudentService : IStudent
    {
        private readonly HttpClient client;

        public StudentService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await client.GetFromJsonAsync<Student>($"api/Student/by-id/{id}");
            if (student == null)
            {
                throw new Exception($"Student with ID {id} not found.");
            }
            return student;
        }


        public async Task<List<Student>> GetAllStudentsAsync()
        {

            return await client.GetFromJsonAsync<List<Student>>("api/Student");
        }

        public async Task<Student> CreateStudentAsync(Student student) 
        {
            var response = await client.PostAsJsonAsync("api/Student/add", student);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Student>();
            }
            else
            {
                throw new Exception($"Failed to create new student. Status code: {response.StatusCode}");
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            var response = await client.DeleteAsync($"api/Student/delete/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete student with id {id}. Status code: {response.StatusCode}");
            }
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var response = await client.PutAsJsonAsync($"api/Student/update/{student.Id}", student);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error occurred while trying to update student, message: {response.StatusCode}");
            }
            return await response.Content.ReadFromJsonAsync<Student>();
        }
    }
}
