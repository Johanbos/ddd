using DDD.ValueObjects;

namespace DDD.Events.Base;

[Serializable]
public abstract class DomainEvent(int version = 1)
{
    public int Version { get; init; } = version;
    
    public Guid EventId { get; init; } = Guid.NewGuid(); // NewId.NextGuid();

    public abstract IEnumerable<DomainError> Validate(bool throwOnError = true);
}
