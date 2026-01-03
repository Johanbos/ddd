using DDD.ValueObjects;

namespace DDD.Events.Base;

[Serializable]
public abstract class DomainEvent(int version = 1)
{
    public int Version { get; init; } = version;
    
    public Guid EventId { get; init; } = Guid.NewGuid(); // NewId.NextGuid();

    public abstract IEnumerable<DomainError> Validate();

    public void ValidateAndThrow()
    {
        var errors = Validate();
        if (errors.Any())
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }
    }  
}
