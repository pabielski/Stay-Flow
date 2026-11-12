using Microsoft.AspNetCore.Mvc;
using StayFlow.Services.Features.Hotels.Commands;
using StayFlow.Services.Features.Hotels.Queries;

namespace StayFlow.Api.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelsController : ControllerBase
{
    private readonly GetHotelsHandler _getHotels;
    private readonly GetHotelByIdHandler _getHotelById;
    private readonly CreateHotelHandler _createHotel;
    private readonly UpdateHotelHandler _updateHotel;
    private readonly DeactivateHotelHandler _deactivateHotel;
    public HotelsController(GetHotelsHandler getHotels, GetHotelByIdHandler getHotelById, CreateHotelHandler createHotel, UpdateHotelHandler updateHotel, DeactivateHotelHandler deactivateHotel)
    {
        _getHotels = getHotels;
        _getHotelById = getHotelById;
        _createHotel = createHotel;
        _updateHotel = updateHotel;
        _deactivateHotel = deactivateHotel;
    }
    //get all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var hotels = await _getHotels.HandleAsync();
        return Ok(hotels);
    }
    //get by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var hotel = await _getHotelById.HandleAsync(id);

        if (hotel is null)
        {
            return NotFound();
        }

        return Ok(hotel);
    }
    //create
    [HttpPost]
    public async Task<IActionResult> Create(CreateHotelRequest request)
    {
        var hotel = await _createHotel.HandleAsync(request);

        if (hotel is null)
        {
            return Conflict(new { message = "Hotel o tej nazwie już istnieje w tym mieście." });
        }

        return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
    }
    //update
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateHotelRequest request)
    {
        var result = await _updateHotel.HandleAsync(id, request);

        return result switch
        {
            UpdateHotelResult.Updated => NoContent(),
            UpdateHotelResult.NotFound => NotFound(),
            UpdateHotelResult.Duplicate => Conflict(new { message = "Hotel o tej nazwie już istnieje w tym mieście." }),
            _ => StatusCode(500)
        };
    }
    //delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var found = await _deactivateHotel.HandleAsync(id);

        if (!found)
        {
            return NotFound();
        }

        return NoContent();
    }

}