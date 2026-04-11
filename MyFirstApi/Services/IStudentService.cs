using MyFirstApi.Models;

namespace MyFirstApi.Services;

public interface IStudentService
{
    Task AddStudentAsync(Student student);
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}
