namespace CarTrack_API.Models;

public class VehiclePapers
{
    public int Id { get; set; }
    public required string PaperType { get; set; }
    public DateTime CreationDate { get; set; }
    public Boolean IsActive { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}