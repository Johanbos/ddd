namespace Examples.Test;

using Examples.Application;
using Examples.Events;
using DDD.ValueObjects;

public class DomainEventTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WhenSGReaderLotMetricsRecorded_ThenHaveAnAggregateWithData()
    {
        // setup
        var LotId = "lot-123";
        var service = new LotMetricService();
        var sGReaderLotMetricsRecorded = new SGReaderLotMetricsRecorded
        {
            EventId = Guid.NewGuid(),
            OccurredOnUtc = DateTime.UtcNow,
            LotId = new LotId(LotId),
            Metrics = new Dictionary<string, double>
            {
                { "metric1", 10.5 },
                { "metric2", 20.0 }
            }
        };

        // Act
        service.Execute(sGReaderLotMetricsRecorded);
        var result = service.QueryLotMetrics(LotId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.LotId, Is.EqualTo(sGReaderLotMetricsRecorded.LotId));
    }
}