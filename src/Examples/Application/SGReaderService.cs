using DDD.ValueObjects;
using Examples.Events;
using Examples.Models;

namespace Examples.Application;

public class SGReaderService
{
    // Simulated in-memory storage
    private LotMetrics? LotMetrics = null;
    private bool ReaderStarted;

    public void Execute(SGReaderStart sgReaderStart)
    {
        // Simulate processing the domain command
        
        // the implementation should match the specified version of the event
        if (sgReaderStart.Version != 1)
        {
            throw new NotSupportedException($"Command version {sgReaderStart.Version} is not supported.");
        }

        //validate
        var errors = sgReaderStart.Validate(false);
        if (errors.Any())
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }

        // this should be an idempotent operation, so check if already processed
        var alreadyProcessed = ReaderStarted;
        if (!alreadyProcessed)
        {
            // In a real application, you would have more complex logic here
            // to handle the domain command and update the state of your aggregates.
            ReaderStarted = true;

            // persist changes to the database or event store as needed
        }
        
        // dequeue the command from the message broker
    }

    public void Execute(SGReaderLotMetricsRecorded sgReaderLotMetricsRecorded)
    {
        // Simulate processing the domain event
        
        // the implementation should match the specified version of the event
        if (sgReaderLotMetricsRecorded.Version != 1)
        {
            throw new NotSupportedException($"Event version {sgReaderLotMetricsRecorded.Version} is not supported.");
        }

        // this should be an idempotent operation, so check if already processed
        var alreadyProcessed = LotMetrics != null;
        if (!alreadyProcessed)
        {
            // In a real application, you would have more complex logic here
            // to handle the domain event and update the state of your aggregates.
            LotMetrics = new LotMetrics
            {
                LotId = sgReaderLotMetricsRecorded.LotId,
                Metrics = sgReaderLotMetricsRecorded.Metrics
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
