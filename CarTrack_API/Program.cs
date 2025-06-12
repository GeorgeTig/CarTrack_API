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
using Npgsql;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// Add controllers and API documentation
builder.Services.AddBusinessService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- SECȚIUNEA MODIFICATĂ ---
// Add EF Core DbContext with advanced Npgsql configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Construim un NpgsqlDataSource pentru a putea configura opțiuni avansate
    var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("DefaultConnection"));
    
    // Activăm serializarea dinamică pentru a permite maparea List<T> -> jsonb
    dataSourceBuilder.EnableDynamicJson(); 
    
    var dataSource = dataSourceBuilder.Build();

    // Folosim noul dataSource configurat pentru DbContext
    options.UseNpgsql(dataSource);
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
builder.Services.AddScoped<JwtService>(); // Consideră înlocuirea cu IJwtService dacă nu ai făcut-o deja

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithOrigins("http://localhost:5098", "http://localhost:3000"); 
    });
});

// Add SignalR
builder.Services.AddSignalR();

// Add Quartz & Jobs
builder.Services.AddQuartz(q =>
{
    // Job pentru Remindere
    var reminderJobKey = new JobKey("ReminderJob");
    q.AddJob<ReminderJob>(opt => opt.WithIdentity(reminderJobKey));
    q.AddTrigger(t => t
        .WithIdentity("ReminderJob-trigger")
        .ForJob(reminderJobKey)
        .WithSimpleSchedule(x => x.WithIntervalInMinutes(24).RepeatForever()));
    
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Middleware-ul custom pentru excepții
app.UseMiddleware<ExceptionMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ReminderHub>("/reminderHub");
});

// Swagger doar în Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();