
using DDD.ValueObjects;

namespace DDD.Examples.Models;

public class LotMetrics
{
    public required LotId LotId { get; init; }
    public required Dictionary<string, double> Metrics { get; init; }
}
