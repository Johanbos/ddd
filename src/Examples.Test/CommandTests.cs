namespace Examples.Test;

using Examples.Application;

public class CommandTests
{
    [TestFixture]
    public class ValidCommandTests
    {
        [Test]
        public void Constructor_WithValidLotId_CreatesCommand()
        {
            // Arrange & Act
            var command = new SGReaderStart { AggregateIdentifier = "lot-123" };

            // Assert
            Assert.That(command.AggregateIdentifier, Is.EqualTo("lot-123"));
            Assert.That(command.Version, Is.EqualTo(1));
            Assert.That(command.CommandTypeName, Is.Not.Empty);
            Assert.That(command.CommandId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(command.OccurredOnUtc, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [TestCase("lot-000")]
        [TestCase("lot-001")]
        [TestCase("lot-999")]
        public void Constructor_WithVariousValidLotIds_CreatesCommand(string validLotId)
        {
            // Arrange & Act
            var command = new SGReaderStart { AggregateIdentifier = validLotId };

            // Assert
            Assert.That(command.AggregateIdentifier, Is.EqualTo(validLotId));
            Assert.That(command.Version, Is.EqualTo(1));
        }

        [Test]
        public void Validate_WithValidCommand_ReturnsNoErrors()
        {
            // Arrange
            var command = new SGReaderStart { AggregateIdentifier = "lot-456" };

            // Act
            var errors = command.Validate(false);

            // Assert
            Assert.That(errors, Is.Empty);
        }

        [Test]
        public void Validate_WithValidCommand_DoesNotThrow()
        {
            // Arrange
            var command = new SGReaderStart { AggregateIdentifier = "lot-789" };

            // Act & Assert
            Assert.DoesNotThrow(() => command.Validate(true));
        }
    }

    [TestFixture]
    public class InvalidCommandTests
    {
        [Test]
        public void Validate_WithInvalidLotId_ReturnsErrors()
        {
            // Arrange
            var invalidCommand = new SGReaderStart { AggregateIdentifier = "invalid-lot" };

            // Act
            var errors = invalidCommand.Validate(false);

            // Assert
            Assert.That(errors, Is.Not.Empty);
        }

        [Test]
        public void Validate_WithInvalidLotId_ThrowsAggregateException()
        {
            // Arrange
            var invalidCommand = new SGReaderStart { AggregateIdentifier = "invalid-lot" };

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
            var command = new SGReaderStart { AggregateIdentifier = "lot-111" };

            // Assert
            Assert.That(command.Version, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class CommandMetadataTests
    {
        [Test]
        public void CommandTypeName_ReturnsFullTypeName()
        {
            // Arrange
            var command = new SGReaderStart { AggregateIdentifier = "lot-222" };

            // Act
            var typeName = command.CommandTypeName;

            // Assert
            Assert.That(typeName, Is.Not.Empty);
            Assert.That(typeName, Does.Contain("SGReaderStart"));
        }

        [Test]
        public void CommandId_IsUnique()
        {
            // Arrange & Act
            var command1 = new SGReaderStart { AggregateIdentifier = "lot-333" };
            var command2 = new SGReaderStart { AggregateIdentifier = "lot-333" };

            // Assert
            Assert.That(command1.CommandId, Is.Not.EqualTo(command2.CommandId));
        }

        [Test]
        public void OccurredOnUtc_IsSetToCurrentUtcTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var command = new SGReaderStart { AggregateIdentifier = "lot-444" };
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.That(command.OccurredOnUtc, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(command.OccurredOnUtc, Is.LessThanOrEqualTo(afterCreation));
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
            var command = new SGReaderStart { AggregateIdentifier = "lot-555" };

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
            var originalCommand = new SGReaderStart { AggregateIdentifier = "lot-666" };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.AggregateIdentifier, Is.EqualTo(originalCommand.AggregateIdentifier));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesCommandId()
        {
            // Arrange
            var originalCommand = new SGReaderStart { AggregateIdentifier = "lot-777" };
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
            var originalCommand = new SGReaderStart { AggregateIdentifier = "lot-888" };
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
            var originalCommand = new SGReaderStart { AggregateIdentifier = "lot-999" };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.OccurredOnUtc, Is.EqualTo(originalCommand.OccurredOnUtc).Within(TimeSpan.FromMilliseconds(1)));
        }

        [Test]
        public void SerializeAndDeserialize_WithValidCommand_PreservesAllProperties()
        {
            // Arrange
            var originalCommand = new SGReaderStart { AggregateIdentifier = "lot-500" };
            var json = System.Text.Json.JsonSerializer.Serialize(originalCommand, JsonOptions);

            // Act
            var deserializedCommand = System.Text.Json.JsonSerializer.Deserialize<SGReaderStart>(json, JsonOptions);

            // Assert
            Assert.That(deserializedCommand, Is.Not.Null);
            Assert.That(deserializedCommand!.AggregateIdentifier, Is.EqualTo(originalCommand.AggregateIdentifier));
            Assert.That(deserializedCommand.CommandId, Is.EqualTo(originalCommand.CommandId));
            Assert.That(deserializedCommand.Version, Is.EqualTo(originalCommand.Version));
            Assert.That(deserializedCommand.CommandTypeName, Is.EqualTo(originalCommand.CommandTypeName));
        }
    }
}
