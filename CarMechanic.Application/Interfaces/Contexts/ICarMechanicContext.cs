using CarMechanic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic.Application.Interfaces.Contexts;

public interface ICarMechanicContext
{
    DbSet<CarBrand> CarBrands { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
