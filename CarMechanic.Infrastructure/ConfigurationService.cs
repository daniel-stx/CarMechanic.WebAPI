using Ardalis.GuardClauses;
using CarMechanic.Application.Enums;
using CarMechanic.Application.Interfaces.Contexts;
using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Application.Interfaces.Services;
using CarMechanic.Application.Models.Settings;
using CarMechanic.Infrastructure.Contexts;
using CarMechanic.Infrastructure.Repositories;
using CarMechanic.Infrastructure.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigurationService
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSqlServer(configuration);
        services.ConfigureCosmosDB(configuration);
        services.ConfigureRedis(configuration);
        services.ConfigureRepositories();
        services.ConfigureServices();

        return services;
    }

    private static IServiceCollection ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerSettings = configuration.GetRequiredSection(SectionNames.SqlServer).Get<SqlServerSettings>();

        Guard.Against.Null(sqlServerSettings);

        services.AddDbContext<CarMechanicContext>(options => options.UseSqlServer(sqlServerSettings.ConnectionString));

        services.Configure<SqlServerSettings>(configuration.GetRequiredSection(SectionNames.SqlServer));

        services.AddScoped<ICarMechanicContext, CarMechanicContext>();

        services.AddScoped<ICarMechanicInitialiser, CarMechanicInitialiser>();

        return services;
    }

    private static IServiceCollection ConfigureCosmosDB(this IServiceCollection services, IConfiguration configuration)
    {
        var cosmosDbSettings = configuration.GetRequiredSection(SectionNames.CosmosDB).Get<CosmosDBSettings>();

        Guard.Against.Null(cosmosDbSettings);

        services.Configure<CosmosDBSettings>(configuration.GetRequiredSection(SectionNames.CosmosDB));

        var options = new CosmosClientOptions
        {
            ConsistencyLevel = ConsistencyLevel.BoundedStaleness
        };

        var cosmosClient = new CosmosClient(cosmosDbSettings.ConnectionString, options);

        Guard.Against.Null(cosmosClient);

        var database = cosmosClient.GetDatabase(cosmosDbSettings.DatabaseName);

        Guard.Against.Null(database);

        services.AddSingleton(database);

        return services;
    }

    private static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettings = configuration.GetRequiredSection(SectionNames.Redis).Get<RedisSettings>();

        Guard.Against.Null(redisSettings);

        var redis = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);

        services.AddSingleton<IConnectionMultiplexer>(redis);

        services.Configure<RedisSettings>(configuration.GetRequiredSection(SectionNames.Redis));

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICarBrandRepository, CarBrandRepository>();
        services.AddScoped<ICachedCarBrandRepository, CachedCarBrandRepository>();
        services.AddScoped<ICarReportRepository, CarReportRepository>();

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ICarBrandService, CarBrandService>();

        return services;
    }
}