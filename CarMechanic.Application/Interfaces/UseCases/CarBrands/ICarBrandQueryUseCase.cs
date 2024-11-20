using CarMechanic.Application.UseCases.CarBrands;

namespace CarMechanic.Application.Interfaces.UseCases.CarBrands;

public interface ICarBrandQueryUseCase
{
    Task<CarBrandQueryResult> HandleAsync(CancellationToken cancellationToken = default);
}
