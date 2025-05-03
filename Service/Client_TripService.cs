using apbd_tutorial8.Model;
using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Repository;

namespace apbd_tutorial8.Service;

public class Client_TripService :IClient_TripService
{
    private readonly IClient_TripRepository _clientTripRepository;
    private readonly IClientRepository _clientRepository;

    public Client_TripService(IClient_TripRepository clientTripRepository, IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
        _clientTripRepository = clientTripRepository;
    }

    public async Task<ServiceResult<string>> AddClientTripAsync(int clientId, int tripId)
    {
        var trip =_clientTripRepository.GetTripByIdAsync(tripId);
        if (trip.Result == null)
        { 
            return ServiceResult<string>.Error("Trip not found");
        }
        var client=_clientTripRepository.GetClientByIdAsync(clientId);
        if (client.Result == null)
        {
            return ServiceResult<string>.Error("Client not found");
        }
        if (trip.Result.MaxPeople < _clientTripRepository.GetNumberOfParticipantsAsync(tripId).Result)
        {
            return ServiceResult<string>.Error("No available seet");
        }
        
        var client_trip=await _clientRepository.GetTripByClientIdAsync(clientId);

        var isExist=client_trip.Trips.FirstOrDefault(t => t.IdTrip == tripId);
        if (isExist != null)
        {
            return ServiceResult<string>.Error("Client already registered trip");
        }
        if(!_clientTripRepository.AddTripAsync(clientId,tripId).Result)
        {
            return ServiceResult<string>.Error("Error, filed to add client to trip");
        }
        return ServiceResult<string>.Ok("success");
    }
}