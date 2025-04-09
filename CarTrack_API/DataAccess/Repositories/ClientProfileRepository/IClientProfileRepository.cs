using CarTrack_API.Models;

namespace CarTrack_API.DataAccess.Repositories.ClientProfileRepository;

public interface IClientProfileRepository
{
    Task AddClientProfileAsync(ClientProfile clientProfile);
}