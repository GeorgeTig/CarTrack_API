using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Mapping;

public static class MappingReminder
{
    /// <summary>
    /// Mapează o configurație de mentenanță la un nou obiect Reminder.
    /// Folosit la crearea inițială a reminderelor pentru un vehicul.
    /// Inițializează contoarele de scadență la valorile maxime.
    /// </summary>
    public static Reminder ToReminder(this VehicleMaintenanceConfig config, double vehicleMileage)
    {
        var reminder = new Reminder
        {
            VehicleMaintenanceConfigId = config.Id,
            LastMileageCkeck = vehicleMileage, // Kilometrajul la momentul creării
            LastDateCheck = DateTime.UtcNow,   // Data la momentul creării
            IsActive = true,
            StatusId = 1, // Status inițial: "Up to date"

            // Se setează "zilele rămase" și "km rămași" la intervalele complete din configurație.
            // Aceste valori vor fi scăzute ulterior de job-ul de actualizare.
            DueMileage = config.MileageIntervalConfig,
            DueDate = config.DateIntervalConfig
        };

        return reminder;
    }

    /// <summary>
    /// Mapează o listă de entități Reminder la o listă de DTO-uri pentru răspunsul API.
    /// Această metodă reutilizează maparea individuală pentru a asigura consistența.
    /// </summary>
    public static List<ReminderResponseDto> ToListReminderResponseDto(this List<Reminder> reminders)
    {
        var reminderResponseDtos = new List<ReminderResponseDto>();
        foreach (var reminder in reminders)
        {
            // Reutilizăm logica de mapare single pentru a evita duplicarea codului
            reminderResponseDtos.Add(reminder.ToReminderResponseDto());
        }
        return reminderResponseDtos;
    }

    /// <summary>
    /// Mapează o singură entitate Reminder la un DTO pentru răspunsul API.
    /// Aceasta este maparea cheie care expune datele către frontend.
    /// </summary>
    public static ReminderResponseDto ToReminderResponseDto(this Reminder reminder)
    {
        if (reminder.VehicleMaintenanceConfig == null)
        {
            throw new System.InvalidOperationException("Reminder entity is missing required VehicleMaintenanceConfig data.");
        }

        return new ReminderResponseDto
        {
            ConfigId = reminder.VehicleMaintenanceConfigId,
            StatusId = reminder.StatusId,
            TypeId = reminder.VehicleMaintenanceConfig.MaintenanceTypeId,
            Name = reminder.VehicleMaintenanceConfig.Name,
            TypeName = reminder.VehicleMaintenanceConfig.MaintenanceType.Name,
            MileageInterval = reminder.VehicleMaintenanceConfig.MileageIntervalConfig,
            TimeInterval = reminder.VehicleMaintenanceConfig.DateIntervalConfig,
            LastMileageCheck = reminder.LastMileageCkeck,
            LastDateCheck = reminder.LastDateCheck,
            IsEditable = reminder.VehicleMaintenanceConfig.IsEditable,
            DueMileage = reminder.DueMileage,
            DueDate = reminder.DueDate,
            IsActive = reminder.IsActive,

            // --- LINIA NOUĂ PENTRU MAPARE ---
            IsCustom = reminder.VehicleMaintenanceConfig.IsCustom
        };
    }
}