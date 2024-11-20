using CarMechanic.Application.Interfaces.UseCases.CarReports;
using CarMechanic.Application.UseCases.CarReports;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace CarMechanic.WebAPI.Endpoints.CarRaport;

[HttpPost("/api/car-reports")]
[AllowAnonymous]
public sealed class CreateCarReportEndpoint(ICarReportCommandUseCase carReportCommand) : Ep.Req<CreateCarReportRequest>.Res<CreateCarReportResponse>
{
    public override async Task HandleAsync(CreateCarReportRequest request, CancellationToken cancellationToken)
    {
        var response = await carReportCommand.CreateAsync(request, cancellationToken);

        await SendOkAsync(response, cancellationToken);
    }
}
