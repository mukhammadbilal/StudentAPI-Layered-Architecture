using Microsoft.Data.SqlClient;
using System.Data;

namespace MyFirstApi.Data;

public class ApplicationDbContext(IConfiguration configuration)
{
    private readonly string connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string not found");

    public IDbConnection CreateConnection()
        => new SqlConnection(connectionString);
}
