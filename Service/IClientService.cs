using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Model;

namespace apbd_tutorial8.Service;

public interface IClientService
{
    Task<ServiceResult<Trip_ClientDTO>> GetTripByClientIdAsync(int clientId);
    Task<int> addClientAsync(Client client);
    
}