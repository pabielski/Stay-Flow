using StayFlow.Data.Entities;

namespace StayFlow.Services.Features.Hotels.Dtos;

public static class HotelMappings
{
    public static HotelDto ToDto(this Hotel hotel) => new HotelDto
    {
        Id = hotel.Id,
        Name = hotel.Name,
        City = hotel.City,
        Address = hotel.Address,
        Phone = hotel.Phone,
        Email = hotel.Email,
        CheckInTime = hotel.CheckInTime,
        IsActive = hotel.IsActive
    };
}