namespace CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;

public class VehicleUsageStatsResponseDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Distance { get; set; }
    
}