namespace CarTrack_API.EntityLayer.Models;

public class VehiclePaper
{
    public int Id { get; set; }
    
    public required string PaperType { get; set; }
    
    public DateTime IssueDate { get; set; }
    public int ValabilityDays { get; set; }
    public DateTime ExpiryDate => IssueDate.AddDays(ValabilityDays);
    
    
    public Boolean IsActive { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    
}