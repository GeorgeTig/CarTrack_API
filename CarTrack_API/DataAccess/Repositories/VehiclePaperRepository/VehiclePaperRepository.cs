﻿using CarTrack_API.DataAccess.DataContext;

namespace CarTrack_API.DataAccess.Repositories.VehiclePaperRepository;

public class VehiclePaperRepository(ApplicationDbContext context) : BaseRepository.BaseRepository(context), IVehiclePaperRepository
{
    
}