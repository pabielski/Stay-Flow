using Microsoft.EntityFrameworkCore;
using StayFlow.Data;
using StayFlow.Data.Entities;
using StayFlow.Services.Common.Exceptions;
using StayFlow.Services.Features.Hotels.Dtos;

namespace StayFlow.Services.Features.Hotels.Queries
{
    public class GetHotelByIdHandler
    {
        private readonly StayFlowDbContext _db;

        public GetHotelByIdHandler(StayFlowDbContext db)
        {
            _db = db;
        }
        public async Task<HotelDto> HandleAsync(long id)
        {

             var hotel = await _db.Hotels
                .Where(h => h.Id == id)
                .Select(h => h.ToDto())
                .FirstOrDefaultAsync();
            if (hotel is null)
            {
                throw new NotFoundException($"Nie znaleziono hotelu o id {id}.");
            }
             return hotel;
        }
    }
}
