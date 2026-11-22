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

        return Ok(hotel);
    }
    //create
    [HttpPost]
    public async Task<IActionResult> Create(CreateHotelRequest request)
    {
        var hotel = await _createHotel.HandleAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = hotel.Id }, hotel);
    }
    //update
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateHotelRequest request)
    {
        await _updateHotel.HandleAsync(id, request);
        return NoContent();
        
    }
    //delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
       await _deactivateHotel.HandleAsync(id);

        return NoContent();
    }

}