

using Examples.Events;
using Examples.Models;

namespace Examples.Application;

public class LotMetricService
{
    // Simulated in-memory storage for LotMetrics aggregate
    private LotMetrics? LotMetrics = null;

    public void Execute(SGReaderLotMetricsRecorded sGReaderLotMetricsRecorded)
    {
        // Simulate processing the domain event
        
        // the implementation should match the specified version of the event
        if (sGReaderLotMetricsRecorded.Version != 1)
        {
            throw new NotSupportedException($"Event version {sGReaderLotMetricsRecorded.Version} is not supported.");
        }

        // this should be an idempotent operation, so check if already processed
        var alreadyProcessed = LotMetrics != null;
        if (!alreadyProcessed)
        {
            // In a real application, you would have more complex logic here
            // to handle the domain event and update the state of your aggregates.
            LotMetrics = new LotMetrics
            {
                LotId = sGReaderLotMetricsRecorded.LotId,
                Metrics = sGReaderLotMetricsRecorded.Metrics
            };
            // persist changes to the database or event store as needed
        }
        // dequeue the event from the message broker
    }


    public LotMetrics? QueryLotMetrics(string _)
    {
        // In a real application, you would query the database or read model
        // to retrieve the LotMetrics aggregate by its LotId.
        return LotMetrics;
    }
}