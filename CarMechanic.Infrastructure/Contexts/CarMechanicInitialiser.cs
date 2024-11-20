using CarMechanic.Application.Interfaces.Contexts;
using CarMechanic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CarMechanic.Infrastructure.Contexts;

public sealed class CarMechanicInitialiser(CarMechanicContext context) : ICarMechanicInitialiser
{
    public async Task InitialiseAsync()
    {
        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error("An error occurred while initialising the database.", ex);
            throw;
        }
    }

    public async Task SeedAsync()
    {
        await SeedCarBrandsAsync();
    }

    private async Task SeedCarBrandsAsync()
    {
        var carBrands = new List<CarBrand> {
            new ()
            {
                Id = 0,
                Name = "BMW"
            },
            new ()
            {
                Id = 0,
                Name = "Volvo"
            },
            new ()
            {
                Id = 0,
                Name = "Volkswagen"
            },
            new ()
            {
                Id = 0,
                Name = "Aston Martin"
            },
            new ()
            {
                Id = 0,
                Name = "Ford"
            },
        };

        var carBrandsIds = carBrands.Select(x => x.Name).ToList();

        var carBrandsInDb = await context.CarBrands.Where(x => carBrandsIds.Contains(x.Name)).Select(x => x.Name).ToListAsync();

        var missingCarBrands = carBrands.Where(x => !carBrandsInDb.Contains(x.Name)).ToList();

        if (missingCarBrands.Any())
        {
            await context.CarBrands.AddRangeAsync(missingCarBrands);
            await context.SaveChangesAsync();
        }
    }
}
