namespace DDD;

/// <summary>
/// Represents a domain event that occurs within the domain model.
/// Domain events are used to capture and communicate significant occurrences within the business domain.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the type of the domain event.
    /// </summary>
    string EventTypeName { get; }

    /// <summary>
    /// Gets the version of the domain event schema.
    /// </summary>
    int Version { get; }

    /// <summary>
    /// Gets the date and time (in UTC) when the domain event occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }

    /// <summary>
    /// Gets the unique identifier for this domain event instance.
    /// </summary>
    Guid EventId { get; }
}
