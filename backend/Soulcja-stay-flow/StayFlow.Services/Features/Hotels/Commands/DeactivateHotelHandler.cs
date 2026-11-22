using Microsoft.EntityFrameworkCore;
using StayFlow.Data;
using StayFlow.Services.Common.Exceptions;

namespace StayFlow.Services.Features.Hotels.Commands;

public class DeactivateHotelHandler
{
    private readonly StayFlowDbContext _db;

    public DeactivateHotelHandler(StayFlowDbContext db)
    {
        _db = db;
    }

    public async Task HandleAsync(long id)
    {
        var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

        if (hotel is null)
        {
            throw new NotFoundException($"Nie znaleziono hotelu o id {id}.");

        }

        hotel.IsActive = false;
        await _db.SaveChangesAsync();

    }
}