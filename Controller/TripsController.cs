using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Repository;
using apbd_tutorial8.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial8.Controller;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITrip_CountryService _service;

    public TripsController(ITrip_CountryService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var trips =await _service.GetTripsAsync();
        return Ok(trips);
    }
}