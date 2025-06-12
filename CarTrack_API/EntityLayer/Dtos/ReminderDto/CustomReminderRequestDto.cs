using System.ComponentModel.DataAnnotations;

namespace CarTrack_API.EntityLayer.Dtos.ReminderDto;

public class CustomReminderRequestDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [Range(1, 10)] // Presupunând că ai un număr fix de tipuri de mentenanță
    public int MaintenanceTypeId { get; set; }

    // Utilizatorul trebuie să specifice cel puțin un interval
    [Range(-1, 200000)]
    public int MileageInterval { get; set; }

    [Range(-1, 3650)] // Maxim 10 ani
    public int DateInterval { get; set; }
}