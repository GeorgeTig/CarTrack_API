using CarTrack_API.EntityLayer.Dtos.BodyDto;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Dtos.Usage;
using CarTrack_API.EntityLayer.Dtos.VehicleDto;
using CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;
using CarTrack_API.EntityLayer.Dtos.VehicleInfo;
using CarTrack_API.EntityLayer.Dtos.VehicleModelDto;
using CarTrack_API.EntityLayer.Dtos.VehicleUsageStatsDto;

namespace CarTrack_API.BusinessLogic.Services.VehicleService;

/// <summary>
/// Definește operațiunile de business pentru gestionarea vehiculelor și a datelor asociate.
/// </summary>
public interface IVehicleService
{
    // --- Metode de Securitate ---
    /// <summary>
    /// Verifică dacă un utilizator deține un vehicul specific și activ.
    /// </summary>
    Task<bool> UserOwnsVehicleAsync(int userId, int vehicleId);

    // --- Metode CRUD pentru Vehicul ---
    /// <summary>
    /// Obține o listă cu toate vehiculele active ale unui client.
    /// </summary>
    Task<List<VehicleResponseDto>> GetAllByClientIdAsync(int clientId);

    /// <summary>
    /// Adaugă un vehicul nou în sistem și generează planul de mentenanță default.
    /// </summary>
    Task AddVehicleAsync(VehicleRequestDto request);

    /// <summary>
    /// Dezactivează un vehicul (soft delete).
    /// </summary>
    Task DeactivateVehicleAsync(int vehicleId);

    // --- Metode pentru Date de Utilizare ---
    /// <summary>
    /// Adaugă un log de mentenanță, actualizează kilometrajul și resetează reminderele.
    /// </summary>
    Task AddVehicleMaintenanceAsync(VehicleMaintenanceRequestDto request);
    
    /// <summary>
    /// Adaugă o nouă citire a odometrului (Quick Sync).
    /// </summary>
    Task AddMileageReadingAsync(int vehicleId, AddMileageReadingRequestDto request);

    // --- Metode CRUD pentru Remindere (care necesită contextul vehiculului) ---
    /// <summary>
    /// Adaugă un reminder custom pentru un vehicul.
    /// </summary>
    Task AddCustomReminderAsync(int vehicleId, CustomReminderRequestDto request);

    /// <summary>
    /// Șterge un reminder custom (operațiune ireversibilă, spre deosebire de dezactivare).
    /// </summary>
    Task DeleteCustomReminderAsync(int configId);

    // --- Metode de Citire a Detaliilor Vehiculului ---
    /// <summary>
    /// Obține detaliile motorului pentru un vehicul.
    /// </summary>
    Task<VehicleEngineResponseDto?> GetVehicleEngineByVehicleIdAsync(int vehicleId);

    /// <summary>
    /// Obține detaliile modelului pentru un vehicul.
    /// </summary>
    Task<VehicleModelResponseDto?> GetVehicleModelByVehicleIdAsync(int vehicleId);

    /// <summary>
    /// Obține informațiile generale (kilometraj, etc.) pentru un vehicul.
    /// </summary>
    Task<VehicleInfoResponseDto?> GetVehicleInfoByVehicleIdAsync(int vehicleId);

    /// <summary>
    /// Obține detaliile caroseriei pentru un vehicul.
    /// </summary>
    Task<BodyResponseDto?> GetBodyByVehicleIdAsync(int vehicleId);

    // --- Metode pentru Istoric și Statistici ---
    /// <summary>
    /// Obține istoricul complet de mentenanță pentru un vehicul.
    /// </summary>
    Task<List<MaintenanceLogDto>> GetMaintenanceHistoryAsync(int vehicleId);
    
    /// <summary>
    /// Obține statistici de utilizare zilnică pentru ultima săptămână.
    /// </summary>
    Task<List<DailyUsageDto>> GetDailyUsageForLastWeekAsync(int vehicleId, string timeZoneId);
}