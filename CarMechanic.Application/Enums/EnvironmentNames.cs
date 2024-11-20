namespace CarMechanic.Application.Enums;

public sealed class EnvironmentNames
{
    public static class App
    {
        public static readonly string UseKeyVault = "USE_KEY_VAULT";
    }

    public static class KeyVault
    {
        public static readonly string Url = "KEY_VAULT_URL";
        public static readonly string TenantId = "KEY_VAULT_TENANT_ID";
        public static readonly string ClientId = "KEY_VAULT_CLIENT_ID";
        public static readonly string ClientSecret = "KEY_VAULT_CLIENT_SECRET";
    }
}
