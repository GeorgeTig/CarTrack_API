using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehiclePapers> VehiclePapers { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Mechanic_Service> Mechanic_Services { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Notification> Notifications { get; set; }
}