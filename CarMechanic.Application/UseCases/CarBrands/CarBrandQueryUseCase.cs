using CarMechanic.Application.Interfaces.Services;
using CarMechanic.Application.Interfaces.UseCases.CarBrands;
using CarMechanic.Application.Models.CarBrands;

namespace CarMechanic.Application.UseCases.CarBrands;

public sealed record CarBrandQueryResult(IEnumerable<CarBrandItem> CarBrandItems);

public sealed class CarBrandQueryUseCase(ICarBrandService carBrandService) : ICarBrandQueryUseCase
{
    public async Task<CarBrandQueryResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var brands = await carBrandService.GetCarBrandsAsync(cancellationToken);

        return new CarBrandQueryResult(brands);
    }
}
