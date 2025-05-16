using EZStay.UI.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------
// Add services to the container
// -------------------------------

// Add MVC
builder.Services.AddControllersWithViews();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register HttpClient for EZStay.API
builder.Services.AddHttpClient("EZStayApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7301/api/v1/");
});

// Register API services
builder.Services.AddScoped<UserApiService>();
builder.Services.AddScoped<AuthApiService>(); // Add this line

var app = builder.Build();

// -------------------------------
// Configure HTTP Request Pipeline
// -------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Default 30 days
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static assets

app.UseRouting();

// Add this line before UseAuthorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();