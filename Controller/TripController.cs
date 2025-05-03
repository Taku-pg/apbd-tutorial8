using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Repository;
using apbd_tutorial8.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial8.Controller;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    private readonly ITrip_CountryService _service;

    public TripController(ITrip_CountryService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public ActionResult<Trip_CountryDTO> Get(int id)
    {
        var trips = _service.GetTrips();
        return Ok(trips);
    }
}