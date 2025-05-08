using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.DataAccess.Repositories.ClientProfileRepository;

public interface IClientProfileRepository
{
    Task AddClientProfileAsync(ClientProfile clientProfile);
}