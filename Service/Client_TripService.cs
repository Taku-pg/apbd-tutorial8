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
        var trip = await CheckTripExistence(tripId);
        if (!trip.Success)
        {
            return ServiceResult<string>.Error(trip.Message);
        }
        
        var client = await CheckClientExistence(clientId);
        if (!client.Success)
        {
            return ServiceResult<string>.Error(client.Message);
        }

        if (trip.Data.MaxPeople <= await _clientTripRepository.GetNumberOfParticipantsAsync(tripId))
        {
             return ServiceResult<string>.Error("No available place");
        }
        
        var clientTrip=await _clientRepository.GetTripByClientIdAsync(clientId);
        var isExist=clientTrip.Trips.FirstOrDefault(t => t.IdTrip == tripId);
        if (isExist != null)
        {
            return ServiceResult<string>.Error("Client already registered trip");
        }
        if(!await _clientTripRepository.AddTripAsync(clientId,tripId))
        {
            return ServiceResult<string>.Error("Error, failed to add client to trip");
        }
        return ServiceResult<string>.Ok("success");
    }

    public async Task<ServiceResult<string>> DeleteClientTripAsync(int clientId, int tripId)
    {
        var trip = await CheckTripExistence(tripId);
        if (!trip.Success)
        {
            return ServiceResult<string>.Error(trip.Message);
        }
        
        var client = await CheckClientExistence(clientId);
        if (!client.Success)
        {
            return ServiceResult<string>.Error(client.Message);
        }
        
        var client_trip = await _clientRepository.GetTripByClientIdAsync(clientId);
        var isExist = client_trip.Trips.FirstOrDefault(t => t.IdTrip == tripId);
        if (isExist == null)
        {
             return ServiceResult<string>.Error("Client not registered trip");
        }
        if (!await _clientTripRepository.DeleteTripAsync(clientId, tripId))
        {
            return ServiceResult<string>.Error("Error, failed to delete client from trip");
        }
        return ServiceResult<string>.Ok("success");
    }

    private async Task<ServiceResult<Trip>> CheckTripExistence(int tripId)
    {
        var trip = await _clientTripRepository.GetTripByIdAsync(tripId);
        if (trip == null)
        {
            return ServiceResult<Trip>.Error("Trip not found");
        }
        return ServiceResult<Trip>.Ok(trip);
    }

    private async Task<ServiceResult<Client>> CheckClientExistence(int clientId)
    {
        var client=await _clientTripRepository.GetClientByIdAsync(clientId);
        if (client == null)
        {
            return ServiceResult<Client>.Error("Client not found");
        }
        return ServiceResult<Client>.Ok(client);
    }
}