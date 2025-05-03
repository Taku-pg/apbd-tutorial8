using apbd_tutorial8.Model;
using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Repository;

namespace apbd_tutorial8.Service;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<ServiceResult<Trip_ClientDTO>> GetTripByClientIdAsync(int clientId)
    {
        var client= await _clientRepository.GetTripByClientIdAsync(clientId);
        
        
        if (client == null)
        {
            return ServiceResult<Trip_ClientDTO>.Error("Client not found");
        }
        if (!client.Trips.Any())
        { 
            return ServiceResult<Trip_ClientDTO>.Error("No trips found");
        }
        return ServiceResult<Trip_ClientDTO>.Ok(client);
    }

    public async Task<int> addClientAsync(Client client)
    {
        var id= await _clientRepository.AddClientAsync(client);
        return id;
    }
}