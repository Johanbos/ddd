
using DDD.ValueObjects;

namespace DDD.Examples.Events;

public class SGReaderLotMetricsRecordedV1 : DomainEvent
{
    public SGReaderLotMetricsRecordedV1() : base(1)
    {
    }

    public required LotId LotId { get; init; }
    public required Dictionary<string, double> Metrics { get; init; }
}