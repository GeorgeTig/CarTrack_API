namespace CarTrack_API.EntityLayer.Dtos.VehicleDto;

public class VehicleResponseDto
{
    public int Id { get; set; }
    public string Vin { get; set; }
    public int Mileage { get; set; }
    public required string ModelName { get; set; }
    public int Year { get; set; }
}