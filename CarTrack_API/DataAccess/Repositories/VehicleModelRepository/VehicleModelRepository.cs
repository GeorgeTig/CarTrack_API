using System.Globalization;
using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.VehicleModelRepository;

public class VehicleModelRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleModelRepository
{
    public async Task<List<VehicleModel>> GetAllByVinDtoAsync(VehicleVinDeserializedDto vinDeserializedDto)
{
    // Validare pentru anul vehiculului (Year)
    if (!int.TryParse(vinDeserializedDto.Year, out int vinYear))
    {
        vinYear = 0;
    }

    // Validare pentru puterea motorului (HP)
    if (!int.TryParse(vinDeserializedDto.HP, out int vinHp))
    {
        vinHp = 0;
    }

    
    
    var query = _context.VehicleModel
        .Include(vm => vm.Body)
        .Include(vm => vm.VehicleEngine)
        .Include(vm => vm.Producer)
        .Where(vm => (string.IsNullOrEmpty(vinDeserializedDto.Producer) || vm.Producer.Name.ToLower() == vinDeserializedDto.Producer.ToLower()) &&
                     (string.IsNullOrEmpty(vinDeserializedDto.Body) || vm.Body.BodyType.ToLower() == vinDeserializedDto.Body.ToLower()) &&
                     (string.IsNullOrEmpty(vinDeserializedDto.FuelTypePrimary) || vm.VehicleEngine.EngineType.ToLower() == vinDeserializedDto.FuelTypePrimary.ToLower()) &&
                     (vinYear == 0 || vm.Year == vinYear) && 
                     (string.IsNullOrEmpty(vinDeserializedDto.Series) || vm.SeriesName.ToLower() == vinDeserializedDto.Series.ToLower()) &&
                     (vinHp ==0 || vm.VehicleEngine.Horsepower == vinHp) && 
                     (string.IsNullOrEmpty(vinDeserializedDto.Transmission) || vm.VehicleEngine.Transmission.ToLower() == vinDeserializedDto.Transmission.ToLower()) && 
                     (string.IsNullOrEmpty(vinDeserializedDto.Cylinders) || vm.VehicleEngine.Cylinders.ToLower() == vinDeserializedDto.Cylinders.ToLower()) 
        );
        

    // Executăm interogarea și obținem rezultatele
    var vehicleModels = await query.ToListAsync();

    return vehicleModels;
}
  
}