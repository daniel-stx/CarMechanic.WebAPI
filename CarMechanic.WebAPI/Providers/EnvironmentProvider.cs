using Ardalis.GuardClauses;
using CarMechanic.Application.Enums;

namespace CarMechanic.WebAPI.Providers;

public static class EnvironmentProvider
{
    public static bool GetUseKeyVaultEnvironmentFlag()
    {
        var useKeyVaultEnvironmentVariable = Environment.GetEnvironmentVariable(EnvironmentNames.App.UseKeyVault) ?? string.Empty;

        Guard.Against.NullOrEmpty(useKeyVaultEnvironmentVariable);

        var useKeyVault = useKeyVaultEnvironmentVariable.ToLower() == bool.TrueString.ToLower();

        return useKeyVault;
    }
}
