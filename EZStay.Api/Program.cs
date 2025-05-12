using EZStay.Api.Services;
using EZStay.Api.Data;
using EZStay.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configure EF Core to use SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories (with generic IRepository for better typing)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Register AutoMapper with the correct profile assembly
builder.Services.AddAutoMapper(typeof(EZStay.Api.Mappings.AMProfiles).Assembly);

// Add HttpClient for external API access
builder.Services.AddHttpClient();

// Add Controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline

// Use Swagger only in Development environment (optional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EZStay API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    }
    
    );
}

// Use HTTPS redirection and authorization
app.UseHttpsRedirection();

// Enable authentication and authorization (if used later)
app.UseAuthentication();  // Uncomment if authentication is enabled
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run the application
app.Run();
