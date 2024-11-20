using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Domain.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CarMechanic.Infrastructure.Repositories;

public sealed class CachedCarBrandRepository(IConnectionMultiplexer multiplexer) : ICachedCarBrandRepository
{
    private const string CarBrands = "carbrands:";
    private const string AllCarBrands = "carbrands:all";

    private readonly IDatabase _database = multiplexer.GetDatabase();

    public async Task<IEnumerable<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var redisValue = await _database.StringGetAsync(AllCarBrands);

        if (redisValue.IsNullOrEmpty)
        {
            return [];
        }

        var serializedList = redisValue.ToString();

        var deserializedValue = JsonConvert.DeserializeObject<List<CarBrand>>(serializedList);

        return deserializedValue ?? [];
    }

    public async Task SetGetAllResultAsync(IEnumerable<CarBrand> brands, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var serializedList = JsonConvert.SerializeObject(brands);

        await _database.StringSetAsync(AllCarBrands, serializedList);
    }

    public async Task<CarBrand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var redisValue = await _database.StringGetAsync(CarBrands + id);

        if (redisValue.IsNullOrEmpty)
        {
            return null;
        }

        var serializedObject = redisValue.ToString();

        var deserializedValue = JsonConvert.DeserializeObject<CarBrand>(serializedObject);

        return deserializedValue;
    }

    public async Task SetGetByIdResultAsync(CarBrand brand, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var serializedObject = JsonConvert.SerializeObject(brand);

        await _database.StringSetAsync(CarBrands + brand.Id, serializedObject);
    }
}
