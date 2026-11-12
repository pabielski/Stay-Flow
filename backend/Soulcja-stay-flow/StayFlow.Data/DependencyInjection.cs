using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StayFlow.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddStayFlowData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StayFlowDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}