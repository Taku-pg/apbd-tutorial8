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
    private readonly IClient_TripService _clientTripService;

    public ClientsController(IClientService clientService, IClient_TripService clientTripService)
    {
        _clientService = clientService;
        _clientTripService = clientTripService;
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
        return Ok(client.Result.Data);
    }

    [HttpPost]
    public IActionResult AddTrip([FromBody] Client client)
    {
        var id = _clientService.addClientAsync(client);
        return Ok(id.Result);
    }

    [HttpPut("{id}/trips/{tripId}")]
    public IActionResult UpdateTrip(int id, int tripId)
    {
        var res=_clientTripService.AddClientTripAsync(id, tripId);
        if (!res.Result.Success)
        {
            return BadRequest(res.Result.Message);
        }
        return Ok(res.Result.Data);
    }
}