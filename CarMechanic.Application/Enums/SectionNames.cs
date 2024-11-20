namespace CarMechanic.Application.Enums;

public static class SectionNames
{
    //Key Vault
    public static readonly string SqlServer = "SqlServer";
    public static readonly string CosmosDB = "CosmosDB";
    public static readonly string Redis = "Redis";

    //App
    public static readonly string KeyVault = "KeyVault";

    public static IEnumerable<string> GetKeyVaultSectionNames()
    {
        yield return SqlServer;
        yield return CosmosDB;
        yield return Redis;
    }
}
