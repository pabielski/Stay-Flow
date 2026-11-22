using StayFlow.Data;
using StayFlow.Data.Entities;
using StayFlow.Services.Features.Hotels.Dtos;


namespace StayFlow.Services.Features.Hotels.Commands
{
    public class CreateHotelHandler
    {
        private readonly StayFlowDbContext _db;
        public CreateHotelHandler(StayFlowDbContext db)
        {
            _db = db;
        }
        public async Task<HotelDto> HandleAsync(CreateHotelRequest request)
        {
            var hotel = new Hotel();
            HotelCommandHelper.ApplyTo(hotel, request);

            await HotelCommandHelper.EnsureUniqueAsync(_db, hotel.Name, hotel.City);

            _db.Hotels.Add(hotel);
            await HotelCommandHelper.SaveAsync(_db);

            return hotel.ToDto();



        }

    }

}
