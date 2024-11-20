using CarMechanic.Domain.Entities;

namespace CarMechanic.Application.Interfaces.Repositories;

public interface ICarBrandRepository
{
    Task<CarBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default);
}
