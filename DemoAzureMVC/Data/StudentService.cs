using DemoAzureMVC.Shared;
using Humanizer;
using System.Net.Http;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
            return await client.GetFromJsonAsync<Student>($"api/Student/by-id/{id}");
        }


        public async Task<List<Student>> GetAllStudentsAsync()
        {

            var students = await client.GetFromJsonAsync<List<Student>>("api/Student");

            if (students == null || !students.Any())
            {
                // Logga ett meddelande för att kontrollera om vi faktiskt får någon data
                Console.WriteLine("Ingen studentdata hämtades.");
            }

            return students;
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
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            try
            {
                // Skicka DELETE-förfrågan till API:et
                var response = await client.DeleteAsync($"api/Student/delete/{id}");

                // Kontrollera om begäran var framgångsrik
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Student with id {id} was successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete student with id {id}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while trying to delete student: {ex.Message}");
            }
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var response = await client.PutAsJsonAsync($"api/Student/update/{student.Id}", student);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error occurred while trying to update student");
                throw new Exception($"Error occurred while trying to update student, message: {response.StatusCode}");
            }
            var updatedStudent = await response.Content.ReadFromJsonAsync<Student>();
            return updatedStudent;
        }



    }
}
