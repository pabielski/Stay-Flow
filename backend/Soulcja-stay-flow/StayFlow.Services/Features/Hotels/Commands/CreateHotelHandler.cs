using StayFlow.Data;
using StayFlow.Data.Entities;
using StayFlow.Services.Features.Hotels.Dtos;
using Microsoft.EntityFrameworkCore;


namespace StayFlow.Services.Features.Hotels.Commands
{
    public class CreateHotelHandler
    {
        private readonly StayFlowDbContext _db;

        public CreateHotelHandler(StayFlowDbContext db)
        {
            _db = db;
        }
        public async Task<HotelDto?> HandleAsync(CreateHotelRequest request)
        {
            var name = request.Name.Trim();
            var city = request.City.Trim();

            var alreadyExists = await _db.Hotels
                .AnyAsync(h => h.Name == name && h.City == city);

            if (alreadyExists)
            {
                return null;
            }

            var hotel = new Hotel
            {
                Name = name,
                City = city,
                Address = request.Address.Trim(),
                Phone = request.Phone,
                Email = request.Email,
                CheckInTime = request.CheckInTime
            };

            _db.Hotels.Add(hotel);
            await _db.SaveChangesAsync();
            return hotel.ToDto();

          
        }
    }
}
