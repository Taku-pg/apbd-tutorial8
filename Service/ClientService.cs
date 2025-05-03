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
    
    public async Task<Trip_ClientDTO> GetTripByClientIdAsync(int clientId)
    {
        var client= await _clientRepository.GetTripByClientIdAsync(clientId);
        return client;
    }
}