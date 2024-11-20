using CarMechanic.Application.Interfaces.Contexts;
using CarMechanic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic.Infrastructure.Contexts;

public sealed class CarMechanicContext(DbContextOptions<CarMechanicContext> options) : DbContext(options), ICarMechanicContext
{
    public DbSet<CarBrand> CarBrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
