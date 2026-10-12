namespace Infrastructure;

using Application.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    // Регистрируем все реализации
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IFleetRepository, InMemoryFleetRepository>();
        services.AddSingleton<IServiceCenter, SimpleServiceCenter>();
        services.AddSingleton<FleetService>();

        return services;
    }
}