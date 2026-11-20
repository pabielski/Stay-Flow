using System;
using System.Collections.Generic;
using System.Text;

namespace StayFlow.Services.Features.RoomTypes.Dtos
{
    public class RoomTypeDto
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public short MaxOccupancy { get; set; }
        public decimal SizeM2 { get; set; }
        public bool IsActive { get; set; }

    }
}
