using apbd_tutorial8.Model.DTO;

namespace apbd_tutorial8.Service;

public interface ITrip_CountryService
{
    IEnumerable<Trip_CountryDTO> GetTrips();
}