using apbd_tutorial8.Model;

namespace apbd_tutorial8.Service;

public interface IClient_TripService
{
    Task<ServiceResult<string>> AddClientTripAsync(int clientId, int tripId);
    Task<ServiceResult<string>> DeleteClientTripAsync(int clientId, int tripId);
}