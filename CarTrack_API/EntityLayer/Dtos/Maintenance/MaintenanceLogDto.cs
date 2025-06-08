namespace CarTrack_API.EntityLayer.Dtos.Maintenance;

public class MaintenanceLogDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double Mileage { get; set; }
    public double Cost { get; set; }
    public string ServiceProvider { get; set; }
    public string Notes { get; set; }
    public List<string> PerformedTasks { get; set; } // O listă de string-uri cu task-urile efectuate
}