using FLIP_CRUD.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using FLIP_CRUD.Services.PostService;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotNetEnv.Env.Load();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();



// Console.WriteLine("Hello ${MYSQL_SERVER}");
// Console.WriteLine($"Hello {Environment.GetEnvironmentVariable("MYSQL_SERVER")}");

// Step 3: Replace placeholders in the connection string dynamically
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?.Replace("${MYSQL_SERVER}", Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? "")
    ?.Replace("${MYSQL_PORT}", Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "")
    ?.Replace("${MYSQL_DATABASE}", Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "")
    ?.Replace("${MYSQL_USER}", Environment.GetEnvironmentVariable("MYSQL_USER") ?? "")
    ?.Replace("${MYSQL_PASSWORD}", Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "");


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseMySql(
    defaultConnection,
    ServerVersion.AutoDetect(defaultConnection)
    ));

    // Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));



builder.Services.AddControllers();
builder.Services.AddScoped<IPostService, PostService>();



// CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // Ensure routing is added
app.MapControllers();

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
