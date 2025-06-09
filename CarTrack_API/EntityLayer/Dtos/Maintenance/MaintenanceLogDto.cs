namespace CarTrack_API.EntityLayer.Dtos.Maintenance;

public class MaintenanceLogDto
{
    public int Id { get; set; }
        
    public string EntryType { get; set; }

    public string Date { get; set; }
        
    public double Mileage { get; set; }
        
    public double? Cost { get; set; }
        
    public string? ServiceProvider { get; set; }
        
    public string? Notes { get; set; }
        
    public List<string> PerformedTasks { get; set; }
}