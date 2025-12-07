namespace DDD;

/// <summary>
/// Represents a domain error that occurred during business logic validation or processing.
/// </summary>
/// <remarks>
/// This interface defines the contract for domain-specific errors in the application,
/// allowing for structured error reporting with details about what went wrong and where.
/// </remarks>
public interface IDomainError
{
    /// <summary>
    /// Gets the error code that uniquely identifies the type of error.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Gets the name of the value object that caused the error.
    /// </summary>
    string ValueObjectName { get; }

    /// <summary>
    /// Gets the name of the property associated with this error.
    /// </summary>
    string PropertyName { get; }
}
