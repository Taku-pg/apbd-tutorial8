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
        var client = await _clientService.GetTripByClientIdAsync(id);
        if (!client.Success)
        {
            return NotFound(client.Message);
        }
        return Ok(client.Data);
    }

    [HttpPost]
    public async Task<IActionResult> AddTrip([FromBody] Client client)
    {
        var id =await _clientService.addClientAsync(client);
        return Ok(id);
    }

    [HttpPut("{id}/trips/{tripId}")]
    public async Task<IActionResult> UpdateTrip(int id, int tripId)
    {
        var res=await _clientTripService.AddClientTripAsync(id, tripId);
        if (!res.Success)
        {
            return BadRequest(res.Message);
        }
        return Ok(res.Data);
    }

    [HttpDelete("{id}/trips/{tripId}")]
    public async Task<IActionResult> DeleteTrip(int id, int tripId)
    {
        var res=await _clientTripService.DeleteClientTripAsync(id, tripId);
        if (!res.Success)
        {
            return BadRequest(res.Message);
        }
        return Ok(res.Data);
    }
}