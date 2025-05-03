using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Repository;

namespace apbd_tutorial8.Service;

public class Trip_CountryService : ITrip_CountryService
{
    public readonly ITrip_CountryRepository _repository;

    public Trip_CountryService(ITrip_CountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Trip_CountryDTO>> GetTripsAsync()
    {
        var trips = await _repository.getTripsAsync();
        return trips;
    }
}