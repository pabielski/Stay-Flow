using System;
using System.Collections.Generic;
using System.Text;

namespace StayFlow.Services.Features.Hotels.Dtos
{
    public class HotelDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public TimeOnly CheckInTime { get; set; }
        public bool IsActive { get; set; }
    }
}
