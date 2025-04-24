using CarTrack_API.EntityLayer.Models;
using CarTrack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.DataContext;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Body> Body { get; set; }
    public DbSet<ClientProfile> ClientProfile { get; set; }
    public DbSet<Deal> Deal { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecord { get; set; }
    public DbSet<ManagerProfile> ManagerProfile { get; set; }
    public DbSet<MechanicProfile> MechanicProfile { get; set; }
    public DbSet<Notification> Notification { get; set; }
    public DbSet<Producer> Producer { get; set; }
    public DbSet<RepairShop> RepairShop { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<Vehicle> Vehicle { get; set; }
    public DbSet<VehicleEngine> VehicleEngine { get; set; }
    public DbSet<VehicleModel> VehicleModel { get; set; }
    public DbSet<VehiclePaper> VehiclePaper { get; set; }
    public DbSet<VehicleInfo> VehicleInfo { get; set; }
    public DbSet<VehicleUsageStats> VehicleUsageStats { get; set; }
    public DbSet<VehicleMaintenanceConfig> VehicleMaintenanceConfig { get; set; }
    public DbSet<MaintenanceCategory> MaintenanceCategory { get; set; }
    public DbSet<MaintenanceType> MaintenanceType { get; set; }
    public DbSet<Reminder> Reminder { get; set; }
    
 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<ClientProfile>()
            .HasKey(cp => cp.UserId);
        
        modelBuilder.Entity<ClientProfile>()
            .HasOne(cp => cp.User)
            .WithOne(u => u.ClientProfile)
            .HasForeignKey<ClientProfile>(cp => cp.UserId);

        modelBuilder.Entity<MechanicProfile>()
            .HasKey(mp => mp.UserId);
        
        modelBuilder.Entity<MechanicProfile>()
            .HasOne(mp => mp.User)
            .WithOne(u => u.MechanicProfile)
            .HasForeignKey<MechanicProfile>(mp => mp.UserId);
        
        modelBuilder.Entity<MechanicProfile>()
            .HasOne(m => m.RepairShop)
            .WithMany(r => r.Mechanics)
            .HasForeignKey(m => m.RepairShopId);

        modelBuilder.Entity<ManagerProfile>()
            .HasKey(mp => mp.UserId);
        
        modelBuilder.Entity<ManagerProfile>()
            .HasOne(mp => mp.User)
            .WithOne(u => u.ManagerProfile)
            .HasForeignKey<ManagerProfile>(mp => mp.UserId);
        
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.Client)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.ClientId);
        
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.VehicleModel)
            .WithMany(vm => vm.Vehicles)
            .HasForeignKey(v => v.VehicleModelId);
        
        modelBuilder.Entity<Vehicle>()
            .HasMany(v => v.Reminders)
            .WithOne(r => r.Vehicle)
            .HasForeignKey(r => r.VehicleId);
        
        modelBuilder.Entity<VehicleMaintenanceConfig>()
            .HasOne(vc => vc.Vehicle)
            .WithMany(v => v.VehicleMaintenanceConfigs)
            .HasForeignKey(vc => vc.VehicleId);
        
        modelBuilder.Entity<VehicleMaintenanceConfig>()
            .HasOne(vm => vm.MaintenanceCategory)
            .WithMany(mc => mc.VehicleMaintenanceConfigs)
            .HasForeignKey(vm => vm.MaintenanceCategoryId);
        
        modelBuilder.Entity<VehicleMaintenanceConfig>()
            .HasOne(vm => vm.MaintenanceType)
            .WithMany(mt => mt.VehicleMaintenanceConfigs)
            .HasForeignKey(vm => vm.MaintenanceTypeId);
        
        modelBuilder.Entity<VehicleInfo>()
            .HasKey(vi => vi.VehicleId);
        
        modelBuilder.Entity<VehicleInfo>()
            .HasOne(vi => vi.Vehicle)
            .WithOne(v => v.VehicleInfo)
            .HasForeignKey<VehicleInfo>(vi => vi.VehicleId);
        
        modelBuilder.Entity<VehicleUsageStats>()
            .HasOne(vu => vu.Vehicle)
            .WithMany(v => v.VehicleUsageStats)
            .HasForeignKey(vu => vu.VehicleId);
        
        modelBuilder.Entity<VehicleModel>()
            .HasOne(vm => vm.VehicleEngine)
            .WithMany(e => e.VehicleModels)
            .HasForeignKey(vm => vm.VehicleEngineId);
        
        modelBuilder.Entity<VehicleModel>()
            .HasOne(vm => vm.Body)
            .WithMany(b => b.VehicleModels)
            .HasForeignKey(vm => vm.BodyId);
        
        modelBuilder.Entity<VehicleModel>()
            .HasOne(vm => vm.Producer)
            .WithMany(p => p.VehicleModels)
            .HasForeignKey(vm => vm.ProducerId);
        
        modelBuilder.Entity<VehiclePaper>()
            .HasOne(vp => vp.Vehicle)
            .WithMany(v => v.VehiclePapers)
            .HasForeignKey(vp => vp.VehicleId);
        
        modelBuilder.Entity<RepairShop>()
            .HasOne(r => r.Manager)
            .WithMany(m => m.RepairShops)
            .HasForeignKey(r => r.ManagerId);
        
        modelBuilder.Entity<Deal>()
            .HasOne(d => d.RepairShop)
            .WithMany(s => s.Deals)
            .HasForeignKey(d => d.RepairShopId);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.RepairShop)
            .WithMany(r => r.Appointments)
            .HasForeignKey(a => a.RepairShopId);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Mechanic)
            .WithMany(m => m.Appointments)
            .HasForeignKey(a => a.MechanicId);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Vehicle)
            .WithMany(v => v.Appointments)
            .HasForeignKey(a => a.VehicleId);
        
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.MaintenanceRecord)
            .WithMany(m => m.Appointments)
            .HasForeignKey(a => a.MaintenanceRecordId);
        
        modelBuilder.Entity<Appointment>()
            .HasMany(a => a.Deals)
            .WithMany(d => d.Appointments)
            .UsingEntity<Dictionary<string, object>>(
                "AppointmentDeal", 
                j => j.HasOne<Deal>().WithMany().HasForeignKey("DealId"),
                j => j.HasOne<Appointment>().WithMany().HasForeignKey("AppointmentId") 
            );

        modelBuilder.Entity<MaintenanceRecord>()
            .HasOne(m => m.Vehicle)
            .WithOne(v => v.MaintenanceRecord)
            .HasForeignKey<MaintenanceRecord>(m => m.VehicleId);
        
        base.OnModelCreating(modelBuilder);
    }
}