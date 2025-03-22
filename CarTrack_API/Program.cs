using System.Text;
using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services;
using CarTrack_API.DataAccess.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); 
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Add AutoMapper

builder.Services.AddDbContext<ApplicationDbContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}); // Add Database Connection

var jwtKey = builder.Configuration["Jwt:Key"]; // Get the key from appsettings.json
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)); // Create a SymmetricSecurityKey

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Add authentication
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = key
        };
    });

builder.Services.AddAuthorization(); // Add authorization
builder.Services.AddScoped<JwtTokenService>(); // Add JwtTokenService

var app = builder.Build();

app.UseAuthentication(); // Use authentication
app.UseAuthorization(); // Use authorization
app.MapControllers(); // Map controllers

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
