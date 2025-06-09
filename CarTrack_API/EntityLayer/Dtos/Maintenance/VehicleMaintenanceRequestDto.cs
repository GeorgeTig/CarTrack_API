using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.EntityLayer.Dtos.Maintenance;

public class MaintenanceItemInput
{
    public int? ConfigId { get; set; }
    public string? CustomName { get; set; }
}
    
public class VehicleMaintenanceRequestDto
{
    public int VehicleId { get; set; }
    public DateTime Date { get; set; }
    public double Mileage { get; set; }
        
    // Folosim noua clasă complexă
    public List<MaintenanceItemInput> MaintenanceItems { get; set; } = new(); 

    public string ServiceProvider { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public decimal Cost { get; set; } = 0; 
        
    // Am adăugat și câmpurile noi, opționale
    public string? EntryType { get; set; }
    public List<string>? AttachmentUrls { get; set; }
}
