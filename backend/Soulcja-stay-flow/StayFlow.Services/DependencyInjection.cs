using Microsoft.Extensions.DependencyInjection;
using StayFlow.Services.Features.Hotels.Commands;
using StayFlow.Services.Features.Hotels.Queries;
using StayFlow.Services.Features.RoomTypes.Queries;

namespace StayFlow.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddStayFlowServices(this IServiceCollection services)
    {
        // Hotele
        services.AddScoped<GetHotelsHandler>();
        services.AddScoped<GetHotelByIdHandler>();
        services.AddScoped<CreateHotelHandler>();
        services.AddScoped<UpdateHotelHandler>();
        services.AddScoped<DeactivateHotelHandler>();

        //RoomTypes
        services.AddScoped<GetRoomTypesHandler>();

        return services;
    }
}