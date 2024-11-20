using CarMechanic.Application.Interfaces.UseCases.CarReports;
using CarMechanic.Application.UseCases.CarReports;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace CarMechanic.WebAPI.Endpoints.CarRaport;

[HttpGet("/api/car-reports")]
[AllowAnonymous]
public sealed class GetAllCarRaportEndpoint(ICarReportQueryUseCase carReportQuery) : Ep.NoReq.Res<GetAllCarReportResponse>
{
    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var response = await carReportQuery.GetAllAsync(cancellationToken);

        await SendOkAsync(response, cancellationToken);
    }
}