using apbd_tutorial8.Model.DTO;

namespace apbd_tutorial8.Service;

public interface ITrip_CountryService
{
    Task<IEnumerable<Trip_CountryDTO>> GetTripsAsync();
}