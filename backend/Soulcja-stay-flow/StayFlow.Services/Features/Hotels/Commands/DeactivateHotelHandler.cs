using Microsoft.EntityFrameworkCore;
using StayFlow.Data;

namespace StayFlow.Services.Features.Hotels.Commands;

public class DeactivateHotelHandler
{
    private readonly StayFlowDbContext _db;

    public DeactivateHotelHandler(StayFlowDbContext db)
    {
        _db = db;
    }

    public async Task<bool> HandleAsync(long id)
    {
        var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

        if (hotel is null)
        {
            return false;
        }

        hotel.IsActive = false;
        hotel.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return true;
    }
}