using MyFirstApi.Models;
using MyFirstApi.Repositories;

namespace MyFirstApi.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    public async Task AddStudentAsync(Student student)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student));
        }

        if (string.IsNullOrEmpty(student.FirstName))
        {
            throw new ArgumentException("First name is required");
        }

        await studentRepository.AddStudentAsync(student);
    }

    public async Task<List<Student>> GetAllStudentsAsync() =>
        await studentRepository.GetAllStudentsAsync();


    public async Task<Student> GetStudentByIdAsync(int id)
    {
        var student = await studentRepository.GetStudentByIdAsync(id);

        if (student == null)
        {
            throw new KeyNotFoundException($"Student with ID {id} not found.");
        }

        return student;
    }

    public async Task UpdateStudentAsync(Student student)
    {
        if (student == null || student.Id <= 0)
        {
            throw new ArgumentException("Invalid student data.");
        }

        await studentRepository.UpdateStudentAsync(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid ID.");
        }

        await studentRepository.DeleteStudentAsync(id);
    }
}
