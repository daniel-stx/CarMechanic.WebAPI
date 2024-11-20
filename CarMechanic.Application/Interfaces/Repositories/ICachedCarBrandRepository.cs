using CarMechanic.Domain.Entities;

namespace CarMechanic.Application.Interfaces.Repositories;

public interface ICachedCarBrandRepository
{
    Task<IEnumerable<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task SetGetAllResultAsync(IEnumerable<CarBrand> brands, CancellationToken cancellationToken = default);
    Task SetGetByIdResultAsync(CarBrand brand, CancellationToken cancellationToken = default);
}
