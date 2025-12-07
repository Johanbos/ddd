
namespace DDD;

/// <summary>
/// Defines a contract for string-based value objects.
/// </summary>
/// <remarks>
/// Implementations of this interface represent immutable value objects that encapsulate string values
/// and provide value-based equality semantics.
/// </remarks>
public interface IStringValueObject
{
    /// <summary>
    /// Gets the string value encapsulated by this value object.
    /// </summary>
    string Value { get; }

    /// <summary>
    /// Determines whether the current instance is equal to the specified <see cref="StringValueObject"/>.
    /// </summary>
    /// <param name="other">The <see cref="StringValueObject"/> to compare with the current instance.</param>
    /// <returns><c>true</c> if the current instance and <paramref name="other"/> are equal; otherwise, <c>false</c>.</returns>
    bool Equals(StringValueObject? other);

    /// <summary>
    /// Determines whether the specified object is equal to the current instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
    bool Equals(object? obj);

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    int GetHashCode();

    /// <summary>
    /// Returns a string representation of the current instance.
    /// </summary>
    /// <returns>A string that represents the current instance.</returns>
    string ToString();
}
