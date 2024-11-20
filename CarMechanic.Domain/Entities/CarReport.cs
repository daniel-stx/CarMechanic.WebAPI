using Newtonsoft.Json;

namespace CarMechanic.Domain.Entities;

public sealed class CarReport
{
    [JsonProperty(PropertyName = "id")]
    public required string Id { get; set; }

    public required string CarId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Summary { get; set; }

    public DateTime Created { get; set; }
}
