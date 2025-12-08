namespace Examples.Test;

using Examples.Application;
using Examples.Events;
using DDD.ValueObjects;

public class EventTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void WhenSGReaderLotMetricsRecorded_ThenHaveAnAggregateWithData()
    {
        // setup
        var lotId = new LotId("lot-123");
        var service = new SGReaderService();
        var sGReaderLotMetricsRecorded = new SGReaderLotMetricsRecorded
        {
            AggregateIdentifier = lotId.Value,
            Metrics = new Dictionary<string, double>
            {
                { "metric1", 10.5 },
                { "metric2", 20.0 }
            }
        };

        // Act
        service.Execute(sGReaderLotMetricsRecorded);
        var result = service.QueryLotMetrics(lotId.Value);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.LotId, Is.EqualTo(lotId));
    }

    [TestFixture]
    public class SerializationTests
    {
        private static readonly System.Text.Json.JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        };

        [Test]
        public void Serialize_WithValidEvent_ProducesValidJson()
        {
            // Arrange
            var @event = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-456"),
                Metrics = new Dictionary<string, double>
                {
                    { "temperature", 25.5 },
                    { "humidity", 65.0 }
                }
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(@event, JsonOptions);

            // Assert
            Assert.That(json, Is.Not.Null.And.Not.Empty);
            Assert.That(json, Does.Contain("lot-456"));
            Assert.That(json, Does.Contain("temperature"));
            Assert.That(json, Does.Contain("eventId"));
            Assert.That(json, Does.Contain("version"));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidEvent_PreservesLotId()
        {
            // Arrange
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = "lot-789",
                Metrics = new Dictionary<string, double>
                {
                    { "metric1", 10.5 }
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.AggregateIdentifier, Is.Not.Null);
            Assert.That(deserializedEvent.AggregateIdentifier, Is.EqualTo(originalEvent.AggregateIdentifier));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidEvent_PreservesMetrics()
        {
            // Arrange
            var metrics = new Dictionary<string, double>
            {
                { "metric1", 10.5 },
                { "metric2", 20.0 },
                { "metric3", 30.75 }
            };
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-111"),
                Metrics = metrics
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.Metrics, Is.Not.Null);
            Assert.That(deserializedEvent.Metrics.Count, Is.EqualTo(metrics.Count));
            Assert.That(deserializedEvent.Metrics["metric1"], Is.EqualTo(10.5));
            Assert.That(deserializedEvent.Metrics["metric2"], Is.EqualTo(20.0));
            Assert.That(deserializedEvent.Metrics["metric3"], Is.EqualTo(30.75));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidEvent_PreservesEventId()
        {
            // Arrange
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-222"),
                Metrics = new Dictionary<string, double>
                {
                    { "metric1", 15.0 }
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.EventId, Is.EqualTo(originalEvent.EventId));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidEvent_PreservesVersion()
        {
            // Arrange
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-333"),
                Metrics = new Dictionary<string, double>
                {
                    { "metric1", 25.0 }
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.Version, Is.EqualTo(originalEvent.Version));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidEvent_PreservesOccurredOnUtc()
        {
            // Arrange
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-444"),
                Metrics = new Dictionary<string, double>
                {
                    { "metric1", 35.0 }
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.OccurredOnUtc, Is.EqualTo(originalEvent.OccurredOnUtc).Within(TimeSpan.FromMilliseconds(1)));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidEvent_PreservesAllProperties()
        {
            // Arrange
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-555"),
                Metrics = new Dictionary<string, double>
                {
                    { "pressure", 1013.25 },
                    { "altitude", 100.5 }
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.AggregateIdentifier, Is.EqualTo(originalEvent.AggregateIdentifier));
            Assert.That(deserializedEvent.Metrics, Is.EqualTo(originalEvent.Metrics));
            Assert.That(deserializedEvent.EventId, Is.EqualTo(originalEvent.EventId));
            Assert.That(deserializedEvent.Version, Is.EqualTo(originalEvent.Version));
            Assert.That(deserializedEvent.EventTypeName, Is.EqualTo(originalEvent.EventTypeName));
        }

        [Test]
        public void SerializeAndDeserialize_WithEmptyMetrics_PreservesEmptyDictionary()
        {
            // Arrange
            var originalEvent = new SGReaderLotMetricsRecorded
            {
                AggregateIdentifier = ("lot-666"),
                Metrics = new Dictionary<string, double>()
            };
            var json = System.Text.Json.JsonSerializer.Serialize(originalEvent, JsonOptions);

            // Act
            var deserializedEvent = System.Text.Json.JsonSerializer.Deserialize<SGReaderLotMetricsRecorded>(json, JsonOptions);

            // Assert
            Assert.That(deserializedEvent, Is.Not.Null);
            Assert.That(deserializedEvent!.Metrics, Is.Not.Null);
            Assert.That(deserializedEvent.Metrics.Count, Is.EqualTo(0));
        }
    }
}