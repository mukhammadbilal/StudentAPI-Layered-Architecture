using MyFirstApi.Data;
using MyFirstApi.Repositories;
using MyFirstApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IStudentRepository,  StudentRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();

var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
