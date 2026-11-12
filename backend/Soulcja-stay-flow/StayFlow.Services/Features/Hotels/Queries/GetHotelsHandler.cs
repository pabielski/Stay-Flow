using Microsoft.EntityFrameworkCore;
using StayFlow.Data;
using StayFlow.Services.Features.Hotels.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace StayFlow.Services.Features.Hotels.Queries;

public class GetHotelsHandler
{
    private readonly StayFlowDbContext _db;

    public GetHotelsHandler(StayFlowDbContext db)
    {
        _db = db;
    }

    public async Task<List<HotelDto>> HandleAsync()
    {
        return await _db.Hotels
            .OrderBy(h => h.Name)
            .Select(h => h.ToDto())
            .ToListAsync();
    }
}