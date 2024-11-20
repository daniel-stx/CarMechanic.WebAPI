namespace CarMechanic.Application.Models.Settings;

public sealed class KeyVaultSettings
{
    public required string Url { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string TenantId { get; set; }
}
