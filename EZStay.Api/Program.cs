using EZStay.Api.Services;
using EZStay.Api.Data;
using EZStay.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------
// Add services to the container
// -------------------------------

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

// -------------------------------
// Add API Versioning
// -------------------------------
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// -------------------------------
// Add Controllers
// -------------------------------
builder.Services.AddControllers();

// -------------------------------
// Add JWT Authentication
// -------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Set to true if you want to enforce HTTPS
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// -------------------------------
// Add Authorization
// -------------------------------
builder.Services.AddAuthorization();

// -------------------------------
// Swagger/OpenAPI
// -------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------------------
// Configure Middleware
// -------------------------------

// Register the custom exception handler middleware
app.UseMiddleware<EZStay.Api.Middleware.ExceptionHandler>(); // Prefer putting this at the top of the pipeline

// Use Swagger only in Development environment (optional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "EZStay API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Run the application
app.Run();
