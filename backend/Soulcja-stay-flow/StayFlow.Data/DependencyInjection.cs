using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StayFlow.Data.Identity;
namespace StayFlow.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddStayFlowData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<StayFlowDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql => npgsql.MigrationsHistoryTable("__ef_migrations_history", "identity")).UseSnakeCaseNamingConvention());
        services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

        return services;
    }

}