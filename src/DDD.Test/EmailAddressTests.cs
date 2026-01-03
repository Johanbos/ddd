using DDD.ValueObjects;

namespace DDD.Test;

public class EmailAddressTests
{
    [TestFixture]
    public class ValidEmailTests
    {
        [Test]
        public void Constructor_WithValidEmail_CreatesEmail()
        {
            var email = new EmailAddress("user@example.com");

            Assert.That(email.Value, Is.EqualTo("user@example.com"));
        }

        [Test]
        public void Constructor_WithUppercaseInput_NormalizesToLowercase()
        {
            var email = new EmailAddress("USER@Example.COM");

            Assert.That(email.Value, Is.EqualTo("user@example.com"));
        }

        [Test]
        public void Constructor_WithWhitespace_TrimsAndCreates()
        {
            var email = new EmailAddress("  user@example.com  ");

            Assert.That(email.Value, Is.EqualTo("user@example.com"));
        }

        [TestCase("a@b.co")]
        [TestCase("firstname.lastname@domain.com")]
        [TestCase("user+tag@sub.domain.org")]
        public void Constructor_WithVariousValidFormats_CreatesEmail(string valid)
        {
            var email = new EmailAddress(valid);

            Assert.That(email.Value, Is.EqualTo(valid.Trim().ToLowerInvariant()));
        }
    }

    [TestFixture]
    public class EqualityTests
    {
        [Test]
        public void Equals_WithSameEmail_ReturnsTrue()
        {
            var e1 = new EmailAddress("user@example.com");
            var e2 = new EmailAddress("user@example.com");

            Assert.That(e1, Is.EqualTo(e2));
        }

        [Test]
        public void Equals_WithDifferentEmail_ReturnsFalse()
        {
            var e1 = new EmailAddress("user1@example.com");
            var e2 = new EmailAddress("user2@example.com");

            Assert.That(e1, Is.Not.EqualTo(e2));
        }

        [Test]
        public void Equals_WithNormalization_ReturnsTrue()
        {
            var e1 = new EmailAddress("USER@EXAMPLE.COM");
            var e2 = new EmailAddress("user@example.com");

            Assert.That(e1, Is.EqualTo(e2));
        }

        [Test]
        public void GetHashCode_WithSameEmail_ReturnsSameHashCode()
        {
            var e1 = new EmailAddress("user@example.com");
            var e2 = new EmailAddress("user@example.com");

            Assert.That(e1.GetHashCode(), Is.EqualTo(e2.GetHashCode()));
        }
    }

    [TestFixture]
    public class ToStringTests
    {
        [Test]
        public void ToString_ReturnsValue()
        {
            var email = new EmailAddress("user@example.com");

            var result = email.ToString();

            Assert.That(result, Is.EqualTo("user@example.com"));
        }
    }

    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void CanBeUsedInHashSet()
        {
            var e1 = new EmailAddress("user@example.com");
            var e2 = new EmailAddress("user@example.com");
            var e3 = new EmailAddress("other@example.com");

            var set = new HashSet<EmailAddress> { e1, e2, e3 };

            Assert.That(set.Count, Is.EqualTo(2));
        }
    }
}
