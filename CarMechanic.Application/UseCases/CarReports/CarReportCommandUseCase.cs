using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Application.Interfaces.UseCases.CarReports;
using CarMechanic.Domain.Entities;

namespace CarMechanic.Application.UseCases.CarReports;

public sealed record CreateCarReportRequest(string CarId, string Title, string Description, string? Summary);
public sealed record CreateCarReportResponse(string Id, string CarId, string Title, string Description, string? Summary);

public sealed class CarReportCommandUseCase(ICarReportRepository repository) : ICarReportCommandUseCase
{
    public async Task<CreateCarReportResponse> CreateAsync(CreateCarReportRequest request, CancellationToken cancellationToken = default)
    {
        var carReport = new CarReport
        {
            Id = Guid.NewGuid().ToString(),
            CarId = request.CarId,
            Description = request.Description,
            Title = request.Title,
            Created = DateTime.Now,
            Summary = request.Summary,
        };

        var newReport = await repository.CreateAsync(carReport, cancellationToken);

        return new CreateCarReportResponse(newReport.Id, newReport.CarId, newReport.Title, newReport.Description, newReport.Summary);
    }
}
