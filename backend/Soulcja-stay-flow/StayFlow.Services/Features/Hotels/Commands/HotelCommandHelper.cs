using Microsoft.EntityFrameworkCore;
using Npgsql;
using StayFlow.Data;
using StayFlow.Data.Entities;
using StayFlow.Services.Common.Exceptions;

namespace StayFlow.Services.Features.Hotels.Commands
{
    internal static class HotelCommandHelper
    {
        private const string DuplicateMessage = "Hotel o tej nazwie już istnieje w tym mieście.";


        public static void ApplyTo(Hotel hotel, HotelRequestBase request)
        {
            hotel.Name = request.Name.Trim();
            hotel.City = request.City.Trim();
            hotel.Address = request.Address.Trim();
            hotel.Phone = NormalizeOptional(request.Phone);
            hotel.Email = NormalizeOptional(request.Email)?.ToLowerInvariant();
            hotel.CheckInTime = request.CheckInTime;
        }
        public static async Task EnsureUniqueAsync(StayFlowDbContext db, string name, string city, long? excludeId = null)
        {
            var exists = await db.Hotels.AnyAsync(h =>
                h.Name == name &&
                h.City == city &&
                (excludeId == null || h.Id != excludeId));

            if (exists)
            {
                throw new ConflictException(DuplicateMessage);
            }

        }
        public static async Task SaveAsync(StayFlowDbContext db)
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (IsDuplicateHotel(ex))
            {
                throw new ConflictException(DuplicateMessage);
            }
        }
        private static bool IsDuplicateHotel(DbUpdateException ex) =>
       ex.InnerException is PostgresException
       {
           SqlState: PostgresErrorCodes.UniqueViolation,
           ConstraintName: "uq_hotels_name_city"
       };
        private static string? NormalizeOptional(string? value) =>
      string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
