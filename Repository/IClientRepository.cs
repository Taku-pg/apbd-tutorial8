using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Model;

namespace apbd_tutorial8.Repository;

public interface IClientRepository
{
    Task<Trip_ClientDTO> GetTripByClientIdAsync(int id);
    Task AddClientAsync(Client client);
}