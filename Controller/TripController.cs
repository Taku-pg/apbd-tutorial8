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
    public IActionResult Get()
    {
        var trips = _service.GetTripsAsync();
        return Ok(trips.Result);
    }
}