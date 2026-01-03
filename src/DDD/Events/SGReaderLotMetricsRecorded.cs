using DDD.Events.Base;
using DDD.ValueObjects;

namespace DDD.Events;

public class SGReaderLotMetricsRecorded : DomainEvent
{
    public required LotId LotId { get; init; }    
    public required Dictionary<string, double> Metrics { get; init; }

    public override IEnumerable<DomainError> Validate()
    {
        return LotId.Validate();
    }
}