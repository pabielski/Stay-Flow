using StayFlow.Data;
using StayFlow.Services.Features.Hotels.Dtos;
using Microsoft.EntityFrameworkCore;

namespace StayFlow.Services.Features.Hotels.Queries
{
    public class GetHotelByIdHandler
    {
        private readonly StayFlowDbContext _db;

        public GetHotelByIdHandler(StayFlowDbContext db)
        {
            _db = db;
        }
        public async Task<HotelDto?> HandleAsync(long id)
        {
            return await _db.Hotels
                .Where(h => h.Id == id)
                .Select(h => h.ToDto())
                .FirstOrDefaultAsync();
        }
    }
}
