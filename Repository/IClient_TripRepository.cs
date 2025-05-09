using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Model;

namespace apbd_tutorial8.Repository;

public interface IClient_TripRepository
{
    Task<Trip?> GetTripByIdAsync(int id);
    Task<Client?> GetClientByIdAsync(int id);
    Task<int> GetNumberOfParticipantsAsync(int tripId);
    Task<bool> AddTripAsync(int clientId, int tripId);
    Task<bool> DeleteTripAsync(int clientId,int tripId);
    
}