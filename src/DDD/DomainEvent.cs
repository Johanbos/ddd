namespace DDD;

[Serializable]
public abstract class DomainEvent(int version) : IDomainEvent
{
    public string EventType => GetType().FullName ?? string.Empty;
    public int Version { get; init; } = version;
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    public Guid EventId { get; init; } = Guid.NewGuid();
}
