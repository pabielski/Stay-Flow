using StayFlow.Data.Entities;
using StayFlow.Services.Features.Hotels.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StayFlow.Services.Features.RoomTypes.Dtos
{
    public static class RoomTypeMappings
    {
        public static RoomTypeDto ToDto(this RoomType roomType) => new RoomTypeDto
        {
            Id = roomType.Id,
            HotelId = roomType.HotelId,
            Name = roomType.Name,
            Description = roomType.Description,
            MaxOccupancy = roomType.MaxOccupancy,
            SizeM2 = roomType.SizeM2,
            IsActive = roomType.IsActive
        };
    }
}
