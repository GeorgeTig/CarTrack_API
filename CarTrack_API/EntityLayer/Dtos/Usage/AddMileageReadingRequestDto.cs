namespace CarTrack_API.EntityLayer.Dtos.Usage;

public class AddMileageReadingRequestDto
{
    [System.ComponentModel.DataAnnotations.Range(0, 9999999)]
    public double OdometerValue { get; set; }
}