using System.Text;
using CarTrack_API.BusinessLogic.Extensions;
using CarTrack_API.BusinessLogic.Services.JwtService;
using CarTrack_API.BusinessLogic.Services.ReminderService.ReminderHubs;
using CarTrack_API.BusinessLogic.Services.ReminderService.ReminderJobs;
using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// Add controllers and API documentation
builder.Services.AddBusinessService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add EF Core DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

// JWT Authentication setup
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

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();
builder.Services.AddScoped<JwtService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithOrigins("http://localhost:5098");
    });
});

// Add SignalR
builder.Services.AddSignalR();

// Add Quartz & ReminderJob
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("ReminderJob");
    q.AddJob<ReminderJob>(opt => opt.WithIdentity(jobKey));

    q.AddTrigger(t => t
        .WithIdentity("ReminderJob-trigger")
        .ForJob(jobKey)
        .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever()));
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// Adaugă mai întâi UseRouting
app.UseRouting();

// Enable authentication & authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Enable SignalR hub endpoint
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ReminderHub>("/reminderHub");
});

// Enable CORS
app.UseCors("CorsPolicy");

// Use middleware to handle exceptions
app.UseMiddleware<ExceptionMiddleware>();

// Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();