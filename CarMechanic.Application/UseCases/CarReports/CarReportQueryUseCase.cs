using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Application.Interfaces.UseCases.CarReports;
using CarMechanic.Application.Models.CarReports;

namespace CarMechanic.Application.UseCases.CarReports;

public sealed record GetAllCarReportRequest();
public sealed record GetAllCarReportResponse(IEnumerable<CarReportItem> CarReports);

public sealed class CarReportQueryUseCase(ICarReportRepository repository) : ICarReportQueryUseCase
{
    public async Task<GetAllCarReportResponse> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var reports = await repository.GetAllAsync(cancellationToken);

        var carReports = reports.Select(r => new CarReportItem(r.Id, r.CarId, r.Title, r.Created));

        return new GetAllCarReportResponse(carReports);
    }
}
