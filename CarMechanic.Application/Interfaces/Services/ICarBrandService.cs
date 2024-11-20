using CarMechanic.Application.Models.CarBrands;

namespace CarMechanic.Application.Interfaces.Services;

public interface ICarBrandService
{
    Task<IEnumerable<CarBrandItem>> GetCarBrandsAsync(CancellationToken cancellationToken = default);
    Task<CarBrandItem?> GetCarBrandByIdAsync(int id, CancellationToken cancellationToken = default);
}
