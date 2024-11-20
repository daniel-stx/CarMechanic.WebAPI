using CarMechanic.Application.UseCases.CarReports;

namespace CarMechanic.Application.Interfaces.UseCases.CarReports;

public interface ICarReportQueryUseCase
{
    Task<GetAllCarReportResponse> GetAllAsync(CancellationToken cancellationToken = default);
}
