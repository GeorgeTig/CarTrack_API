using CarTrack_API.EntityLayer.Models;

namespace CarTrack_API.BusinessLogic.Services.ClientProfileService;

public interface IClientProfileService
{
    Task AddClientProfileAsync(ClientProfile clientProfile);
}