namespace CarTrack_API.EntityLayer.Dtos.NotificationDto;

public class NotificationResponseDto
{
    public int Id { get; set; }
    public required string Message { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
    public int UserId { get; set; }
    public int VehicleId { get; set; }
    public int ReminderId { get; set; }
    
    public string VehicleName { get; set; } 
    public int VehicleYear { get; set; }
    public string? VehicleImageUrl { get; set; } = null;
}
