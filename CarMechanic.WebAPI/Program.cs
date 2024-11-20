using CarMechanic.WebAPI.Interfaces;
using CarMechanic.WebAPI.Providers;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

var useKeyVault = EnvironmentProvider.GetUseKeyVaultEnvironmentFlag();

if (useKeyVault)
{
    IKeyVaultSecretProvider provider = new KeyVaultSecretProvider();
    await provider.FetchApplicationSecretsAsync(builder.Configuration);
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddFastEndpoints().SwaggerDocument();

var app = builder.Build();

app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();

    using (var scope = app.Services.CreateScope())
    {
        await app.InitializeDatabaseContext(scope);
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
