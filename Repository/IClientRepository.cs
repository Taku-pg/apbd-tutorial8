using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Model;
using apbd_tutorial8.Service;

namespace apbd_tutorial8.Repository;

public interface IClientRepository
{
    Task<Trip_ClientDTO?> GetTripByClientIdAsync(int id);
    Task<int> AddClientAsync(Client client);
}