using StayFlow.Data;
using Microsoft.EntityFrameworkCore;


namespace StayFlow.Services.Features.Hotels.Commands
{ 
public enum UpdateHotelResult
{
    Updated,
    NotFound,
    Duplicate
}
public class UpdateHotelHandler
    {
        private readonly StayFlowDbContext _db;

        public UpdateHotelHandler(StayFlowDbContext db)
        {
            _db = db;
        }

        public async Task<UpdateHotelResult> HandleAsync(long id, UpdateHotelRequest request)
        {
            var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if (hotel is null)
            {
                return UpdateHotelResult.NotFound;
            }

            var name = request.Name.Trim();
            var city = request.City.Trim();

            var duplicateExists = await _db.Hotels
                .AnyAsync(h => h.Id != id && h.Name == name && h.City == city);

            if (duplicateExists)
            {
                return UpdateHotelResult.Duplicate;
            }

            hotel.Name = name;
            hotel.City = city;
            hotel.Address = request.Address.Trim();
            hotel.Phone = request.Phone;
            hotel.Email = request.Email;
            hotel.CheckInTime = request.CheckInTime;
            hotel.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return UpdateHotelResult.Updated;
        }
    }
}
