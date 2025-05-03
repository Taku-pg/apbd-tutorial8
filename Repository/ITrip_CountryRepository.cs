using apbd_tutorial8.Model.DTO;

namespace apbd_tutorial8.Repository;

public interface ITrip_CountryRepository
{
    Task<List<Trip_CountryDTO>> getTripsAsync();
}