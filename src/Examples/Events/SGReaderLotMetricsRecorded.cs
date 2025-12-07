
using DDD;
using DDD.ValueObjects;

namespace Examples.Events;

public class SGReaderLotMetricsRecorded : DomainEvent
{
    public SGReaderLotMetricsRecorded() : base(version: 1)
    {
    }

    public required LotId LotId { get; init; }
    public required Dictionary<string, double> Metrics { get; init; }
}