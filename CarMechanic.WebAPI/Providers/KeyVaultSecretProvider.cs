using Ardalis.GuardClauses;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CarMechanic.Application.Enums;
using CarMechanic.Application.Models.Settings;
using CarMechanic.WebAPI.Interfaces;
using Newtonsoft.Json;

namespace CarMechanic.WebAPI.Providers;

public sealed class KeyVaultSecretProvider : IKeyVaultSecretProvider
{
    public async Task FetchApplicationSecretsAsync(IConfigurationManager configuration)
    {
        var keyVaultSettings = configuration.GetSection(SectionNames.KeyVault).Get<KeyVaultSettings>();

        if (keyVaultSettings == null)
        {
            keyVaultSettings = GetKeyVaultSettingsFromEnvironments();
        }

        var secrets = await FetchSecretsFromAzureKeyVaultAsync(keyVaultSettings);

        InsertSecretsIntoApplicationMemory(configuration, secrets);
    }

    private KeyVaultSettings GetKeyVaultSettingsFromEnvironments()
    {
        var keyVaultUrl = Environment.GetEnvironmentVariable(EnvironmentNames.KeyVault.Url);
        var tenantId = Environment.GetEnvironmentVariable(EnvironmentNames.KeyVault.TenantId);
        var clientId = Environment.GetEnvironmentVariable(EnvironmentNames.KeyVault.ClientId);
        var secret = Environment.GetEnvironmentVariable(EnvironmentNames.KeyVault.ClientSecret);

        Guard.Against.Null(keyVaultUrl);
        Guard.Against.Null(tenantId);
        Guard.Against.Null(clientId);
        Guard.Against.Null(secret);

        var keyVaultSettings = new KeyVaultSettings
        {
            ClientId = clientId,
            ClientSecret = secret,
            TenantId = tenantId,
            Url = keyVaultUrl
        };

        return keyVaultSettings;
    }

    private SecretClient InitializeSecretClient(KeyVaultSettings settings)
    {
        var credential = new ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);
        var client = new SecretClient(new Uri(settings.Url), credential);

        return client;
    }

    private async Task<IEnumerable<KeyVaultSecret>> FetchSecretsFromAzureKeyVaultAsync(KeyVaultSettings settings)
    {
        var client = InitializeSecretClient(settings);
        var sections = SectionNames.GetKeyVaultSectionNames();

        var getSecretTasks = sections.Select(x => client.GetSecretAsync(x));

        var secrets = await Task.WhenAll(getSecretTasks);

        return secrets?.Select(x => x.Value) ?? [];
    }


    private void InsertSecretsIntoApplicationMemory(IConfigurationManager configuration, IEnumerable<KeyVaultSecret> secrets)
    {
        foreach (var item in secrets)
        {
            var deserializedSecret = JsonConvert.DeserializeObject<IDictionary<string, string>>(item.Value);

            Guard.Against.Null(deserializedSecret);

            configuration.AddInMemoryCollection(deserializedSecret!);
        }
    }
}
