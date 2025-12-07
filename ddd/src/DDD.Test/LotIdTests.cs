using DDD.ValueObjects;

namespace DDD.Test;

public class LotIdTests
{
    [TestFixture]
    public class ValidLotIdTests
    {
        [Test]
        public void Constructor_WithValidFormat_CreatesLotId()
        {
            // Arrange & Act
            var lotId = new LotId("lot-123");

            // Assert
            Assert.That(lotId.Value, Is.EqualTo("lot-123"));
        }

        [Test]
        public void Constructor_WithUppercaseInput_NormalizesToLowercase()
        {
            // Arrange & Act
            var lotId = new LotId("LOT-456");

            // Assert
            Assert.That(lotId.Value, Is.EqualTo("lot-456"));
        }

        [Test]
        public void Constructor_WithMixedCaseInput_NormalizesToLowercase()
        {
            // Arrange & Act
            var lotId = new LotId("LoT-789");

            // Assert
            Assert.That(lotId.Value, Is.EqualTo("lot-789"));
        }

        [Test]
        public void Constructor_WithWhitespace_TrimsAndCreates()
        {
            // Arrange & Act
            var lotId = new LotId("  lot-100  ");

            // Assert
            Assert.That(lotId.Value, Is.EqualTo("lot-100"));
        }

        [TestCase("lot-000")]
        [TestCase("lot-001")]
        [TestCase("lot-999")]
        public void Constructor_WithValidThreeDigitNumbers_CreatesLotId(string validValue)
        {
            // Arrange & Act
            var lotId = new LotId(validValue);

            // Assert
            Assert.That(lotId.Value, Is.EqualTo(validValue));
        }
    }

    [TestFixture]
    public class InvalidLotIdTests
    {
        [Test]
        public void Constructor_WithNullValue_ThrowsAggregateException()
        {
            // Arrange & Act & Assert
            Assert.Throws<AggregateException>(() => new LotId(null!));
        }

        [Test]
        public void Constructor_WithEmptyString_ThrowsAggregateException()
        {
            // Arrange & Act & Assert
            Assert.Throws<AggregateException>(() => new LotId(""));
        }

        [Test]
        public void Constructor_WithWhitespaceOnly_ThrowsAggregateException()
        {
            // Arrange & Act & Assert
            Assert.Throws<AggregateException>(() => new LotId("   "));
        }

        [TestCase("lot-1")]       // Too few digits
        [TestCase("lot-12")]      // Too few digits
        [TestCase("lot-1234")]    // Too many digits
        [TestCase("lot-12345")]   // Too many digits
        public void Constructor_WithIncorrectDigitCount_ThrowsAggregateException(string invalidValue)
        {
            // Arrange & Act & Assert
            Assert.Throws<AggregateException>(() => new LotId(invalidValue));
        }

        [TestCase("bar-123")]     // Wrong prefix
        [TestCase("lot_123")]     // Wrong separator
        [TestCase("lot 123")]     // Wrong separator
        [TestCase("lot123")]      // Missing separator
        [TestCase("lot-abc")]     // Non-numeric digits
        [TestCase("lot-12a")]     // Mixed alphanumeric
        public void Constructor_WithInvalidFormat_ThrowsAggregateException(string invalidValue)
        {
            // Arrange & Act & Assert
            if (invalidValue == "LOT-123")
            {
                // This one should pass due to normalization
                Assert.DoesNotThrow(() => new LotId(invalidValue));
            }
            else
            {
                Assert.Throws<AggregateException>(() => new LotId(invalidValue));
            }
        }
    }

    [TestFixture]
    public class TryCreateTests
    {
        [Test]
        public void TryCreate_WithValidValue_ReturnsTrueAndCreatesLotId()
        {
            // Arrange & Act
            var result = LotId.TryCreate("lot-250", out var lotId, out _);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(lotId, Is.Not.Null);
            Assert.That(lotId!.Value, Is.EqualTo("lot-250"));
        }

        [Test]
        public void TryCreate_WithInvalidValue_ReturnsFalseAndLotsIdIsNull()
        {
            // Arrange & Act
            var result = LotId.TryCreate("invalid-lot", out var lotId, out _);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(lotId, Is.Null);
        }

        [Test]
        public void TryCreate_WithNullValue_ReturnsFalseAndLotsIdIsNull()
        {
            // Arrange & Act
            var result = LotId.TryCreate(null!, out var lotId);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(lotId, Is.Null);
        }

        [Test]
        public void TryCreate_WithEmptyString_ReturnsFalseAndLotsIdIsNull()
        {
            // Arrange & Act
            var result = LotId.TryCreate("", out var lotId);

            // Assert
            Assert.That(result, Is.False);
            Assert.That(lotId, Is.Null);
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
