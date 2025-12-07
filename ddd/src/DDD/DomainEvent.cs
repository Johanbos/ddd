namespace DDD;

[Serializable]
public abstract class DomainEvent(int version)
{
    public string EventType => GetType().FullName ?? string.Empty;
    public int Version { get; init; } = version;
    public required DateTime OccurredOnUtc { get; init; }
    public required Guid EventId { get; init; }
}
