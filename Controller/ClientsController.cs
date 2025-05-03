using apbd_tutorial8.Model;
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
    public async Task<IActionResult> GetTripByClientId(int id)
    {
        if (id == 0)
            return BadRequest();
        var client = _clientService.GetTripByClientIdAsync(id);
        if (!client.Result.Success)
        {
            return NotFound(new{message=client.Result.Message});
        }
        return Ok(client.Result);
    }

    [HttpPost]
    public IActionResult AddTrip([FromBody] Client client)
    {
        var id = _clientService.addClientAsync(client);
        return Ok(id.Result);
    }
}