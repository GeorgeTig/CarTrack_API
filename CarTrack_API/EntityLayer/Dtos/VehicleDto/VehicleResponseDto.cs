namespace CarTrack_API.EntityLayer.Dtos.VehicleDto;

public class VehicleResponseDto
{
    public int Id { get; set; }
    public string Vin { get; set; }
    public required string Series { get; set; }
    public int Year { get; set; }
    public required string Producer { get; set; }
}