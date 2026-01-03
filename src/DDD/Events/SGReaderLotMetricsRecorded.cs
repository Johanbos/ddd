using DDD.Events.Base;
using DDD.ValueObjects;

namespace DDD.Events;

public class SGReaderLotMetricsRecorded : DomainEvent
{
    public required LotId LotId { get; init; }    
    public required Dictionary<string, double> Metrics { get; init; }

    public override IEnumerable<DomainError> Validate(bool throwOnError = true)
    {
        List<DomainError> errors = [];
        errors.AddRange(LotId.Validate());

        if (throwOnError && errors.Count != 0)
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }

        return errors;
    }
}