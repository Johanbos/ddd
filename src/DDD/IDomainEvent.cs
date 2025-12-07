namespace DDD;

public interface IDomainEvent
{
    string EventType { get; }
    int Version { get; }
    DateTime OccurredOnUtc { get; }
    Guid EventId { get; }
}
