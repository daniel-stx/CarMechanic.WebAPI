using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Application.Interfaces.Services;
using CarMechanic.Application.Models.CarBrands;

namespace CarMechanic.Infrastructure.Services;

public sealed class CarBrandService(ICarBrandRepository dbRepository, ICachedCarBrandRepository cacheRepository) : ICarBrandService
{
    public async Task<IEnumerable<CarBrandItem>> GetCarBrandsAsync(CancellationToken cancellationToken = default)
    {
        var cachedBrands = await cacheRepository.GetAllAsync(cancellationToken);

        if (cachedBrands.Any())
        {
            return cachedBrands.Select(x => new CarBrandItem(x.Id, x.Name));
        }

        var brands = await dbRepository.GetAllAsync(cancellationToken);
        await cacheRepository.SetGetAllResultAsync(brands);

        return brands.Select(x => new CarBrandItem(x.Id, x.Name));
    }

    public async Task<CarBrandItem?> GetCarBrandByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var brand = await cacheRepository.GetByIdAsync(id, cancellationToken);

        if (brand is not null)
        {
            return new CarBrandItem(brand.Id, brand.Name);
        }

        var dbBrand = await dbRepository.GetByIdAsync(id, cancellationToken);

        if (dbBrand is null)
        {
            return null;
        }

        await cacheRepository.SetGetByIdResultAsync(dbBrand, cancellationToken);

        return new CarBrandItem(dbBrand.Id, dbBrand.Name);
    }
}
