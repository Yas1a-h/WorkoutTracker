using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Services;
using WorkoutTracker.Data;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=workout.db"));
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); // add this line to serve default files like index.html
app.UseStaticFiles();  // add this line to serve static files like CSS, JS, images, etc.
app.UseAuthorization();

app.MapControllers();

app.Run();
