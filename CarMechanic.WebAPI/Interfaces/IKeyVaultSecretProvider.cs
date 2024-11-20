namespace CarMechanic.WebAPI.Interfaces;

public interface IKeyVaultSecretProvider
{
    Task FetchApplicationSecretsAsync(IConfigurationManager configuration);
}
