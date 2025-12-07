
using DDD.ValueObjects;

namespace Examples.Models;

public class LotMetrics
{
    public required LotId LotId { get; init; }
    public required Dictionary<string, double> Metrics { get; init; }
}
