namespace CarTrack_API.EntityLayer.Dtos.ReminderDto;

public class ReminderResponseDto
{
    public int ConfigId { get; set; }
    public int StatusId { get; set; }
    public int TypeId { get; set; }
    
    public required string Name { get; set; }
    public required string TypeName { get; set; }
    
    public int MileageInterval { get; set; }
    public int TimeInterval { get; set; }
    
    public double DueMileage { get; set; }
    public int DueDate { get; set; } 
    
    public bool IsEditable { get; set; }
    public bool IsActive { get; set; }
    public bool IsCustom { get; set; } 

    
    public double LastMileageCheck { get; set; }
    public DateTime LastDateCheck { get; set; }
}