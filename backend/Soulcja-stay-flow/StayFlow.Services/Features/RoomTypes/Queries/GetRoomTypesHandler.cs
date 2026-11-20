using StayFlow.Data;
using StayFlow.Services.Features.Hotels.Dtos;
using Microsoft.EntityFrameworkCore;
using StayFlow.Services.Features.RoomTypes.Dtos;
namespace StayFlow.Services.Features.RoomTypes.Queries
{
    public class GetRoomTypesHandler
    {
        private readonly StayFlowDbContext _db;

        public GetRoomTypesHandler( StayFlowDbContext db)
        {
            _db = db;
        }

        public async Task<List<RoomTypeDto>> HandleAsync(long hotelId)
        {
            return await _db.RoomTypes.OrderBy(r => r.Name).Where(r => r.HotelId == hotelId).Select(r => r.ToDto()).ToListAsync();
        }

    }
}
