using CarMechanic.Domain.Entities;

namespace CarMechanic.Application.Interfaces.Repositories;

public interface ICarReportRepository
{
    Task<IEnumerable<CarReport>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarReport?> GetByIdAsync(string id, int carId, CancellationToken cancellationToken = default);
    Task<CarReport> CreateAsync(CarReport carRaport, CancellationToken cancellationToken = default);
    Task<CarReport> UpdateAsync(CarReport carRaport, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, string carId, CancellationToken cancellationToken = default);
}
