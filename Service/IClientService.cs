using apbd_tutorial8.Model.DTO;

namespace apbd_tutorial8.Service;

public interface IClientService
{
    Task<Trip_ClientDTO> GetTripByClientIdAsync(int clientId);
    
}