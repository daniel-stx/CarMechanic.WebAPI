using CarMechanic.Application.UseCases.CarReports;

namespace CarMechanic.Application.Interfaces.UseCases.CarReports;

public interface ICarReportCommandUseCase
{
    Task<CreateCarReportResponse> CreateAsync(CreateCarReportRequest request, CancellationToken cancellationToken = default);
}
