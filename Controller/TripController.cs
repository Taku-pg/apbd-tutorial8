using apbd_tutorial8.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial8.Controller;

[Route("api/[controller]")]
[ApiController]
public class TripController : ControllerBase
{
    [HttpGet]
    public ActionResult<Trip_CountryDTO> Get(int id)
    {
        
    }
}