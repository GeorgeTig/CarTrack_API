using CarTrack_API.DataAccess.DataContext;
using CarTrack_API.EntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTrack_API.DataAccess.Repositories.VehicleEngineRepository;

public class VehicleEngineRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehicleEngineRepository
{
    
}