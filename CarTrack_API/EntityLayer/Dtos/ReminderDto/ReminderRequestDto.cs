namespace CarTrack_API.EntityLayer.Dtos.ReminderDto;

public class ReminderRequestDto
{
    public int Id { get; set; }
    public int MileageInterval { get; set; }
    public int TimeInterval { get; set; } // in days
}