namespace CarMechanic.Application.Models.Settings;

public sealed class CosmosDBSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}
