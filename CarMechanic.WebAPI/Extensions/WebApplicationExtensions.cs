using Ardalis.GuardClauses;
using CarMechanic.Application.Interfaces.Contexts;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> InitializeDatabaseContext(this WebApplication webApplication, IServiceScope scope)
    {
        var initialiser = scope.ServiceProvider.GetService<ICarMechanicInitialiser>();

        Guard.Against.Null(initialiser);

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();

        return webApplication;
    }
}

