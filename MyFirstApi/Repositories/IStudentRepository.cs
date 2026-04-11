using MyFirstApi.Models;

namespace MyFirstApi.Repositories;

public interface IStudentRepository
{
    Task AddStudentAsync(Student student);
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(int id);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);
}
