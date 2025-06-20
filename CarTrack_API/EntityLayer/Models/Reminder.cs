﻿namespace CarTrack_API.EntityLayer.Models;

public class Reminder
{
    public int VehicleMaintenanceConfigId { get; set; }
    public VehicleMaintenanceConfig VehicleMaintenanceConfig { get; set; }
  
    public double LastMileageCkeck { get; set; }
    public DateTime LastDateCheck { get; set; }
    
    public double DueMileage { get; set; }
    public int DueDate { get; set; } // in days, from the last check
    
    public int StatusId { get; set; }
    public Status Status { get; set; }
    
    public List<Notification> Notifications { get; set; }
    
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; } = false;
}