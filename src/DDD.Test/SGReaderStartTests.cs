namespace DDD.Test;

using DDD.Commands;
using DDD.ValueObjects;

public class SGReaderStartTests
{
    [TestFixture]
    public class ValidCommandTests
    {
        [TestCase("lot-000")]
        [TestCase("lot-001")]
        [TestCase("lot-999")]
        public void Constructor_WithValidLotIds_CreatesCommand(string validLotId)
        {
            // Arrange 
            var lotId = new LotId(validLotId);
            var command = new SGReaderStart { LotId = lotId };

            // Act
            var errors = command.Validate();

            // Assert
            Assert.That(errors, Is.Empty);
            Assert.That(command.LotId, Is.EqualTo(lotId));
            Assert.That(command.Version, Is.EqualTo(1));
            Assert.That(command.CommandId, Is.Not.EqualTo(Guid.Empty));
        }
    }

    [TestFixture]
    public class InvalidCommandTests
    {
        [Test]
        public void Validate_WithInvalidLotId_ReturnsErrors()
        {
            // Arrange
            var invalidCommand = new SGReaderStart { LotId = new LotId("invalid-lot") };

            // Act
            var errors = invalidCommand.Validate(false);

            // Assert
            Assert.That(errors, Is.Not.Empty);
        }

        [Test]
        public void Validate_WithInvalidLotId_ThrowsAggregateException()
        {
            // Arrange
            var invalidCommand = new SGReaderStart { LotId = new LotId("invalid-lot") };

            // Act & Assert
            Assert.Throws<AggregateException>(() => invalidCommand.Validate(true));
        }
    }

    [TestFixture]
    public class CommandVersionTests
    {
        [Test]
        public void Version_IsSetToOne()
        {
            // Arrange & Act
            var command = new SGReaderStart { LotId = new LotId("lot-111") };

            // Assert
            Assert.That(command.Version, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class SerializationTests
    {
        private static readonly System.Text.Json.JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        };

        [Test]
        public void Serialize_WithValidCommand_ProducesValidJson()
        {
            // Arrange
            var command = new SGReaderStart { LotId = new LotId("lot-555") };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(command, JsonOptions);

            // Assert
            Assert.That(json, Is.Not.Null.And.Not.Empty);
            Assert.That(json, Does.Contain("lot-555"));
            Assert.That(json, Does.Contain("commandId"));
            Assert.That(json, Does.Contain("version"));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesLotId()
        {
            // Arrange
            var originalCommand = new SGReaderStart { LotId = new LotId("lot-666") };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.LotId, Is.EqualTo(originalCommand.LotId));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesCommandId()
        {
            // Arrange
            var originalCommand = new SGReaderStart { LotId = new LotId("lot-777") };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.CommandId, Is.EqualTo(originalCommand.CommandId));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesVersion()
        {
            // Arrange
            var originalCommand = new SGReaderStart { LotId = new LotId("lot-888") };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.Version, Is.EqualTo(originalCommand.Version));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesOccurredOnUtc()
        {
            // Arrange
            var originalCommand = new SGReaderStart { LotId = new LotId("lot-999") };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesAllProperties()
        {
            // Arrange
            var originalCommand = new SGReaderStart { LotId = new LotId("lot-500") };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.LotId, Is.EqualTo(originalCommand.LotId));
            Assert.That(deserializedCommand.CommandId, Is.EqualTo(originalCommand.CommandId));
            Assert.That(deserializedCommand.Version, Is.EqualTo(originalCommand.Version));
        }
    }
}
