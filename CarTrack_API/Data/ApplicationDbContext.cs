using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.Data;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehiclePapers> VehiclePapers { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Notification> Notifications { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Vehicles)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.VehiclePapers)
            .WithOne(vp => vp.User)
            .HasForeignKey(vp => vp.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Appointments)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.MaintenanceRecords)
            .WithOne(mr => mr.User)
            .HasForeignKey(mr => mr.UserId);
        
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.Engine)
            .WithMany(e => e.Vehicles)
            .HasForeignKey(v => v.EngineId);
        
        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.VehiclePapers)
            .WithOne(vp => vp.Vehicle)
            .HasForeignKey(vp => vp.VehicleId);
        
        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.MaintenanceRecords)
            .WithOne(mr => mr.Vehicle)
            .HasForeignKey(mr => mr.VehicleId);
        
        modelBuilder.Entity<Service>()
            .HasMany(s => s.Appointments)
            .WithOne(a => a.Service)
            .HasForeignKey(a => a.ServiceId);
        
        modelBuilder.Entity<Service>()
            .HasMany(s => s.Workers)
            .WithMany(ms => ms.Services)
            .UsingEntity(j=> j.ToTable("Worker_Service"));
        
        modelBuilder.Entity<Service>()
            .HasMany(s => s.MaintenanceRecords)
            .WithOne(mr => mr.Service)
            .HasForeignKey(mr => mr.ServiceId);
        
        
        base.OnModelCreating(modelBuilder);
    }
}