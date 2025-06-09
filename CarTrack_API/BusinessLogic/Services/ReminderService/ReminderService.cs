using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.DataAccess.Repositories.ReminderRepository;
using CarTrack_API.EntityLayer.Dtos.Maintenance;
using CarTrack_API.EntityLayer.Dtos.ReminderDto;
using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ReminderService;

public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }
        
        public async Task AddReminderAsync(VehicleMaintenanceConfig vehicleMaintenanceConfig, double vehicleMileage)
        {
            var reminder = MappingReminder.ToReminder(vehicleMaintenanceConfig, vehicleMileage);
            await _reminderRepository.AddAsync(reminder);
        }
        
        public async Task<List<ReminderResponseDto>> GetAllRemindersByVehicleIdAsync(int vehicleId)
        {
            var reminders = await _reminderRepository.GetAllByVehicleIdAsync(vehicleId);
            return reminders.ToListReminderResponseDto();
        }
        
        public async Task<ReminderResponseDto> GetReminderByReminderIdAsync(int reminderId)
        {
            var reminder = await _reminderRepository.GetReminderByReminderIdAsync(reminderId);
            return reminder.ToReminderResponseDto();
        }

        public async Task UpdateReminderAsync(ReminderRequestDto reminderRequest)
        {
            await _reminderRepository.UpdateReminderAsync(reminderRequest);
        }
   
        // --- METODA REFACTORIZATĂ ---
        public async Task UpdateReminderAsync(VehicleMaintenanceRequestDto vehicleMaintenanceRequest)
        {
            // Extragem ID-urile configurațiilor care corespund reminderelor efectuate
            var configIdsToReset = vehicleMaintenanceRequest.MaintenanceItems
                .Where(item => item.ConfigId.HasValue)
                .Select(item => item.ConfigId.Value)
                .ToList();
            
            // Dacă nu a fost efectuată nicio lucrare legată de un reminder, ieșim
            if (!configIdsToReset.Any())
            {
                return;
            }

            // Delegăm logica de resetare către repository
            await _reminderRepository.ResetRemindersAsync(
                vehicleMaintenanceRequest.VehicleId,
                configIdsToReset,
                vehicleMaintenanceRequest.Mileage,
                vehicleMaintenanceRequest.Date
            );
        }
    
        public async Task UpdateReminderActiveAsync(int reminderId)
        {
            await _reminderRepository.UpdateReminderActiveAsync(reminderId);
        }
   
        public async Task ActualizeRemindersDueAsync()
        {
            await _reminderRepository.ActualizeRemindersDueAsync();
        }
    }