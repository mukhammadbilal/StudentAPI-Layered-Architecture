using MyFirstApi.Exceptions;
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
            throw new ValidationException("Student is null");

        if (string.IsNullOrWhiteSpace(student.FirstName))
            throw new ValidationException("First name is required.");

        if (string.IsNullOrWhiteSpace(student.LastName))
            throw new ValidationException("Last name is required.");

        if (student.Age <= 0)
            throw new ValidationException("Age must be a positive number.");

        await studentRepository.AddStudentAsync(student);
    }

    public async Task<List<Student>> GetAllStudentsAsync() =>
        await studentRepository.GetAllStudentsAsync();


    public async Task<Student> GetStudentByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException("Invalid student ID.");
        }

        var student = await studentRepository.GetStudentByIdAsync(id);

        if (student == null)
        {
            throw new NotFoundException($"Student with ID {id} not found.");
        }

        return student;
    }

    public async Task UpdateStudentAsync(Student student)
    {
        if (student.Id <= 0)
        {
            throw new ValidationException("Invalid student ID.");
        }

        var maybeStudent = await studentRepository.GetStudentByIdAsync(student.Id);

        if (maybeStudent == null)
        {
            throw new NotFoundException($"Student with ID {student.Id} not found.");
        }

        await studentRepository.UpdateStudentAsync(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException("Invalid student ID.");
        }

        var maybeStudent = await studentRepository.GetStudentByIdAsync(id);

        if (maybeStudent == null)
        {
            throw new NotFoundException($"Student with ID {id} not found.");
        }

        await studentRepository.DeleteStudentAsync(id);
    }
}
