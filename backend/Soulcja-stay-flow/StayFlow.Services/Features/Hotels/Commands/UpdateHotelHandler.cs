using Microsoft.EntityFrameworkCore;
using StayFlow.Data;
using StayFlow.Services.Common.Exceptions;


namespace StayFlow.Services.Features.Hotels.Commands
{

    public class UpdateHotelHandler
    {
        private readonly StayFlowDbContext _db;
        public UpdateHotelHandler(StayFlowDbContext db)
        {
            _db = db;
        }

        public async Task HandleAsync(long id, UpdateHotelRequest request)

        {
            var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if (hotel is null)
            {
                throw new NotFoundException($"Nie znaleziono hotelu o id {id}.");

            }

            HotelCommandHelper.ApplyTo(hotel, request);

            await HotelCommandHelper.EnsureUniqueAsync(_db, hotel.Name, hotel.City, excludeId: id);

            await HotelCommandHelper.SaveAsync(_db);


        }
    }
}
