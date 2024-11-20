using CarMechanic.Application.Enums;
using CarMechanic.Application.Interfaces.Repositories;
using CarMechanic.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CarMechanic.Infrastructure.Repositories;

public sealed class CarReportRepository : ICarReportRepository
{
    private readonly Container _container;

    public CarReportRepository(Database database)
    {
        var containerProperties = new ContainerProperties(ContainerNames.CarReports, $"/{nameof(CarReport.CarId)}");

        var containerTask = database.CreateContainerIfNotExistsAsync(containerProperties);

        containerTask.Wait();

        var response = containerTask.Result;

        _container = response.Container;
    }

    public async Task<IEnumerable<CarReport>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var queryDefinition = new QueryDefinition($"SELECT * FROM c");

        var query = _container.GetItemQueryIterator<CarReport>(queryDefinition);

        var results = new List<CarReport>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync(cancellationToken);

            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task<CarReport?> GetByIdAsync(string id, int carId, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _container.ReadItemAsync<CarReport>(id, new PartitionKey(carId), cancellationToken: cancellationToken);
            return response;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<CarReport> CreateAsync(CarReport carRaport, CancellationToken cancellationToken = default)
    {
        var response = await _container.CreateItemAsync(carRaport, new PartitionKey(carRaport.CarId), cancellationToken: cancellationToken);

        return response;
    }

    public async Task<CarReport> UpdateAsync(CarReport carRaport, CancellationToken cancellationToken = default)
    {
        var response = await _container.UpsertItemAsync(carRaport, new PartitionKey(carRaport.CarId), cancellationToken: cancellationToken);

        return response;
    }

    public async Task<bool> DeleteAsync(string id, string carId, CancellationToken cancellationToken = default)
    {
        var response = await _container.DeleteItemAsync<CarReport>(id, new PartitionKey(carId), cancellationToken: cancellationToken);

        if (response != null)
        {
            return true;
        }

        return false;
    }
}
