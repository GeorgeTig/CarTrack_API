namespace CarTrack_API.EntityLayer.Dtos.VehicleDto;

public class VehicleRequestDto
{
    public int ClientId { get; set; }
    public int ModelId { get; set; }
    public string Vin { get; set; }
    public double Mileage { get; set; }
    
}