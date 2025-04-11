using System.Text;
using CarTrack_API.BusinessLogic.Extensions;
using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.JwtService;
using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddBusinessService();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

// Configure JWT Authentication
var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Add authorization
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

// Register JwtService
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Enable authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Enable Swagger for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Use middleware to handle exceptions
app.UseMiddleware<ExceptionMiddleware>();

app.Run();