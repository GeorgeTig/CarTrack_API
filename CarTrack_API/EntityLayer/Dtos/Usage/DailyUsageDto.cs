namespace CarTrack_API.EntityLayer.Dtos.Usage;

public class DailyUsageDto
{
    public required string DayLabel { get; set; } 
    
    public double Distance { get; set; }
}