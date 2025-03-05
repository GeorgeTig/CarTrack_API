namespace CarTrack_API.Models;

public class VehiclePaper
{
    public int Id { get; set; }
    public required string PaperType { get; set; }
    public DateTime IssueDate { get; set; }
    public Boolean IsActive { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
}