using System.ComponentModel.DataAnnotations.Schema;

namespace CarTrack_API.EntityLayer.Models;

public class MaintenanceUnverifiedRecord
{
    public int Id { get; set; }
    public DateTime DoneDate { get; set; }
    public double DoneMileage { get; set; }
    public decimal Cost { get; set; }
        
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
        
    public string Description { get; set; }
    public string ServiceProvider { get; set; }
    
    [Column(TypeName = "jsonb")]
    public List<int>? RelatedConfigIds { get; set; } = new();
    
    [Column(TypeName = "jsonb")]
    public List<string>? CustomMaintenanceNames { get; set; } = new();
    
    public string? EntryType { get; set; } = "Scheduled";
    
    [Column(TypeName = "jsonb")]
    public List<string>? AttachmentUrls { get; set; } = new();
}