using DDD.ValueObjects;
using NUnit.Framework.Interfaces;

namespace DDD.Test;

public class LotIdTests
{
    [TestFixture]
    public class ConstructorLotIdTests
    {
        [Test]
        public void Constructor_WithValidFormat_CreatesLotId()
        {
            // Arrange & Act
            var lotId = new LotId("lot-123");

            // Assert
            Assert.That(lotId.Value, Is.EqualTo("lot-123"));
        }
    }

    [TestFixture]
    public class InvalidLotIdTests
    {
        [TestCase("")]
        [TestCase("   ")]
        public void Validate_WithWhitespaceOnly_HasErrors(string invalidValue)
        {
            // Arrange
            var lotId = new LotId(invalidValue);

            // Act
            var errors = lotId.Validate();

            // Assert
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors.Any(e => e.Code == DomainError.CannotBeEmpty), Is.True);
        }

        [TestCase("lot-1")]       // Too few digits
        [TestCase("lot-12")]      // Too few digits
        [TestCase("lot-1234")]    // Too many digits
        [TestCase("lot-12345")]   // Too many digits
        [TestCase("bar-123")]     // Wrong prefix
        [TestCase("lot_123")]     // Wrong separator
        [TestCase("lot 123")]     // Wrong separator
        [TestCase("lot123")]      // Missing separator
        [TestCase("lot-abc")]     // Non-numeric digits
        [TestCase("lot-12a")]     // Mixed alphanumeric
        public void Validate_WithInvalidPattern_HasErrors(string invalidValue)
        {
            // Arrange
            var lotId = new LotId(invalidValue);

            // Act
            var errors = lotId.Validate();

            // Assert
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors.Any(e => e.Code == DomainError.InvalidPattern), Is.True);
        }
    }

    [TestFixture]
    public class EqualityTests
    {
        [Test]
        public void Equals_WithSameLotId_ReturnsTrue()
        {
            // Arrange
            var lotId1 = new LotId("lot-111");
            var lotId2 = new LotId("lot-111");

            // Act & Assert
            Assert.That(lotId1, Is.EqualTo(lotId2));
        }

        [Test]
        public void Equals_WithDifferentLotId_ReturnsFalse()
        {
            // Arrange
            var lotId1 = new LotId("lot-111");
            var lotId2 = new LotId("lot-222");

            // Act & Assert
            Assert.That(lotId1, Is.Not.EqualTo(lotId2));
        }

        [Test]
        public void Equals_WithNormalization_ReturnsTrue()
        {
            // Arrange
            var lotId1 = new LotId("LOT-333");
            var lotId2 = new LotId("lot-333");

            // Act & Assert
            Assert.That(lotId1, Is.EqualTo(lotId2));
        }

        [Test]
        public void GetHashCode_WithSameLotId_ReturnsSameHashCode()
        {
            // Arrange
            var lotId1 = new LotId("lot-444");
            var lotId2 = new LotId("lot-444");

            // Act & Assert
            Assert.That(lotId1.GetHashCode(), Is.EqualTo(lotId2.GetHashCode()));
        }

        [Test]
        public void GetHashCode_WithDifferentLotId_ReturnsDifferentHashCode()
        {
            // Arrange
            var lotId1 = new LotId("lot-444");
            var lotId2 = new LotId("lot-555");

            // Act & Assert
            Assert.That(lotId1.GetHashCode(), Is.Not.EqualTo(lotId2.GetHashCode()));
        }
    }

    [TestFixture]
    public class ToStringTests
    {
        [Test]
        public void ToString_ReturnsValue()
        {
            // Arrange
            var lotId = new LotId("lot-666");

            // Act
            var result = lotId.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("lot-666"));
        }

        [Test]
        public void ToString_WithNormalizedValue_ReturnsLowercase()
        {
            // Arrange
            var lotId = new LotId("LOT-777");

            // Act
            var result = lotId.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("lot-777"));
        }
    }

    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void CanBeUsedInHashSet()
        {
            // Arrange
            var lotId1 = new LotId("lot-888");
            var lotId2 = new LotId("lot-888");
            var lotId3 = new LotId("lot-999");

            // Act
            var set = new HashSet<LotId> { lotId1, lotId2, lotId3 };

            // Assert
            Assert.That(set.Count, Is.EqualTo(2)); // lotId1 and lotId2 are equal
        }

        [Test]
        public void CanBeUsedAsDictionaryKey()
        {
            // Arrange
            var lotId1 = new LotId("lot-101");
            var lotId2 = new LotId("lot-101");

            // Act
            var dict = new Dictionary<LotId, string> { { lotId1, "Lot A" } };
            dict[lotId2] = "Updated Lot A";

            // Assert
            Assert.That(dict.Count, Is.EqualTo(1));
            Assert.That(dict[lotId1], Is.EqualTo("Updated Lot A"));
        }
    }
}
