using apbd_tutorial8.Model.DTO;
using apbd_tutorial8.Repository;
using apbd_tutorial8.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial8.Controller;
[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpGet("{id}/trips")]
    public IActionResult GetTripByClientId(int id)
    {
        var client = _clientService.GetTripByClientIdAsync(id);
        return Ok(client.Result);
    }
        
}