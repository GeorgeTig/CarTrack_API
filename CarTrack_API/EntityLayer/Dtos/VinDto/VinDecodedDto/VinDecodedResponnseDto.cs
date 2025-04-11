namespace CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;

public class VinDecodedResponnseDto
{
   public int ModelId { get; set; }
   public required string SeriesName { get; set; }
   public int Year { get; set; }
   public required string DriveType { get; set; }
   public required string Cylinders { get; set; }
   public double Size { get; set; }
   public int Horsepower { get; set; }
   public int TorqueFtLbs { get; set; }
   public int Doornumber { get; set; }
   public int Seatnumber { get; set; }
}