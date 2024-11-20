namespace CarMechanic.Domain.Abstracts;

public abstract class BaseLookup : BaseEntity
{
    public required string Name { get; set; }
}
