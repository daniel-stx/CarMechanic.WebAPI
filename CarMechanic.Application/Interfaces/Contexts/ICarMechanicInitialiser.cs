namespace CarMechanic.Application.Interfaces.Contexts;

public interface ICarMechanicInitialiser
{
    Task InitialiseAsync();
    Task SeedAsync();
}
