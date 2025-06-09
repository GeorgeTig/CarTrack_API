using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingMaintenanceRecord
{
    /// <summary>
    /// Mapează un DTO de request la modelul de entitate pentru salvare în DB.
    /// Sortează item-urile introduse de utilizator în liste separate și asigură valori default.
    /// </summary>
    public static MaintenanceUnverifiedRecord ToMaintenanceUnverifiedRecord(
        this VehicleMaintenanceRequestDto vehMaintenance)
    {
        var dateAsUtc = DateTime.SpecifyKind(vehMaintenance.Date, DateTimeKind.Utc);

        var maintenance = new MaintenanceUnverifiedRecord
        {
            VehicleId = vehMaintenance.VehicleId,
            DoneDate = dateAsUtc,
            Cost = vehMaintenance.Cost,
            Description = vehMaintenance.Notes,
            DoneMileage = vehMaintenance.Mileage,
            ServiceProvider = vehMaintenance.ServiceProvider,

            RelatedConfigIds = vehMaintenance.MaintenanceItems
                .Where(item => item.ConfigId.HasValue)
                .Select(item => item.ConfigId.Value)
                .ToList(),

            CustomMaintenanceNames = vehMaintenance.MaintenanceItems
                .Where(item => !string.IsNullOrWhiteSpace(item.CustomName))
                .Select(item => item.CustomName)
                .ToList(),

            EntryType = !string.IsNullOrWhiteSpace(vehMaintenance.EntryType)
                ? vehMaintenance.EntryType
                : "Scheduled",

            AttachmentUrls = vehMaintenance.AttachmentUrls ?? new List<string>()
        };

        return maintenance;
    }

    /// <summary>
    /// Mapează o listă de înregistrări de mentenanță la o listă de DTO-uri pentru afișare în istoric.
    /// Această metodă are nevoie de un dicționar pentru a "traduce" ConfigId-urile în nume.
    /// </summary>
    public static List<MaintenanceLogDto> ToMaintenanceLogDtoList(
        this List<MaintenanceUnverifiedRecord> maintenanceRecords,
        Dictionary<int, string> configIdToNameMap)
    {
        var maintenanceLogDtos = new List<MaintenanceLogDto>();
        foreach (var record in maintenanceRecords)
        {
            var performedTasks = new List<string>();

            if (record.RelatedConfigIds != null && record.RelatedConfigIds.Any())
            {
                foreach (var configId in record.RelatedConfigIds)
                {
                    if (configIdToNameMap.TryGetValue(configId, out var taskName))
                    {
                        performedTasks.Add(taskName);
                    }
                    else
                    {
                        performedTasks.Add($"Unknown Maintenance (ID: {configId})");
                    }
                }
            }

            if (record.CustomMaintenanceNames != null && record.CustomMaintenanceNames.Any())
            {
                performedTasks.AddRange(record.CustomMaintenanceNames);
            }

            var maintenanceLogDto = new MaintenanceLogDto
            {
                Id = record.Id,
                EntryType = record.EntryType, // Asignează noul câmp
                Date = record.DoneDate.ToString("yyyy-MM-dd"), // Formatează data ca string
                Mileage = record.DoneMileage,
                Cost = (double?)record.Cost, // Asignează costul ca nullable
                ServiceProvider = record.ServiceProvider, // Asignează ca nullable
                Notes = record.Description, // Asignează ca nullable (folosind Description)
                PerformedTasks = performedTasks
            };
            maintenanceLogDtos.Add(maintenanceLogDto);
        }

        return maintenanceLogDtos;
    }
}