using CarMechanic.Application.Interfaces.Contexts;
using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic.Infrastructure.Repositories;

public sealed class CarBrandRepository(ICarMechanicContext context) : ICarBrandRepository
{
    public async Task<CarBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var brand = await context.CarBrands.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return brand;
    }

    public async Task<IEnumerable<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var brands = await context.CarBrands.ToListAsync(cancellationToken);

        return brands ?? [];
    }
}
