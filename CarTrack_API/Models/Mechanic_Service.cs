namespace CarTrack_API.Models;

public class Mechanic_Service
{
    public int IdService { get; set; }
    public Service Service { get; set; }
    
    public int IdMechanic { get; set; }
    public User Mechanic { get; set; }
}