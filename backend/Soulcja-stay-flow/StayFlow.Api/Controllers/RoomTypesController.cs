using Microsoft.AspNetCore.Mvc;
using StayFlow.Services.Features.RoomTypes.Queries;
using System.ComponentModel.DataAnnotations;

namespace StayFlow.Api.Controllers;

[ApiController]
[Route("api/roomtypes")]
public class RoomTypesController : ControllerBase
    {
    private readonly GetRoomTypesHandler _getRoomTypes;


    public RoomTypesController(GetRoomTypesHandler getRoomTypes)
    {
        _getRoomTypes = getRoomTypes;
    }

    //get all
    [HttpGet]
    public async Task<IActionResult> GetRoomTypes([Required]long? hotelId)
    {
        //if (hotelId is null)
        //{
        //    return BadRequest("Parametr hotelId jest wymagany.");
        //}
        var roomTypes = await _getRoomTypes.HandleAsync(hotelId.Value);
        return Ok(roomTypes);
    }
    }

