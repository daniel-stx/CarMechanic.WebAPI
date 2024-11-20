using CarMechanic.Application.Interfaces.UseCases.CarBrands;
using CarMechanic.Application.Models.CarBrands;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace CarMechanic.WebAPI.Endpoints.CarBrands;

public sealed record GetCarBrandsResponse(IEnumerable<CarBrandItem> CarBrands);

[HttpGet("/api/car-brands")]
[AllowAnonymous]
public sealed class GetCarBrandsEndpoint(ICarBrandQueryUseCase carBrandQuery) : Ep.NoReq.Res<GetCarBrandsResponse>
{
    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var brands = await carBrandQuery.HandleAsync(cancellationToken);

        await SendOkAsync(new GetCarBrandsResponse(brands.CarBrandItems));
    }
}
