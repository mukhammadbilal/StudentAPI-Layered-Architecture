using Dapper;
using MyFirstApi.Data;
using MyFirstApi.Models;
using System.Data;

namespace MyFirstApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            const string sql =
                """
                INSERT INTO Students (FirstName, LastName, Age)
                VALUES (@FirstName, @LastName, @Age)
                """;

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, student);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            const string sql = "SELECT * FROM Students";

            using var connection = context.CreateConnection();
            var result = await connection.QueryAsync<Student>(sql);

            return result.ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Students WHERE Id = @Id";

            using var connection = context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Student>(sql, new { Id = id });
        }

        public async Task UpdateStudentAsync(Student student)
        {
            const string sql =
                """
                UPDATE Students
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Age = @Age
                WHERE Id = @Id
                """;

            using var connection = context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, student);

            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"No student found with ID {student.Id} to update.");
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            const string sql = "DELETE FROM Students WHERE Id = @Id";

            using var connection = context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"No student found with ID {id} to delete.");
            }
        }
    }
}
