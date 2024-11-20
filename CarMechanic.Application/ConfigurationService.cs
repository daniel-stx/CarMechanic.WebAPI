using CarMechanic.Application.Interfaces.UseCases.CarBrands;
using CarMechanic.Application.Interfaces.UseCases.CarReports;
using CarMechanic.Application.UseCases.CarBrands;
using CarMechanic.Application.UseCases.CarReports;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigurationService
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.ConfigureUseCases();

        return services;
    }

    private static IServiceCollection ConfigureUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICarBrandQueryUseCase, CarBrandQueryUseCase>();

        services.AddScoped<ICarReportQueryUseCase, CarReportQueryUseCase>();
        services.AddScoped<ICarReportCommandUseCase, CarReportCommandUseCase>();

        return services;
    }
}
