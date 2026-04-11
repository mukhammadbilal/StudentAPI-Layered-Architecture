using Microsoft.AspNetCore.Mvc;
using MyFirstApi.Data;
using MyFirstApi.Models;
using MyFirstApi.Services;

namespace MyFirstApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService studentService;

    public StudentsController(IStudentService studentService)
    {
        this.studentService = studentService;
    }

    [HttpPost]
    public async Task<IActionResult> PostStudent(Student student)
    {
        try
        {
            await studentService.AddStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An internal error occurred.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        try
        {
            var students = await studentService.GetAllStudentsAsync();
            return Ok(students);
        }
        catch (Exception)
        {
            return StatusCode(500, "Error retrieving data.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        try
        {
            var student = await studentService.GetStudentByIdAsync(id);
            return Ok(student);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent(Student student)
    {
        try
        {
            await studentService.UpdateStudentAsync(student);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Update failed.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            await studentService.DeleteStudentAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Deletion failed.");
        }
    }
}
