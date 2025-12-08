namespace DDD;

/// <summary>
/// Defines the contract for a domain command in a Domain-Driven Design implementation.
/// </summary>
/// <remarks>
/// A domain command represents an instruction to perform a specific action within the domain.
/// It encapsulates the intent and data required to modify the state of the domain.
/// </remarks>
public interface IDomainCommand
{
    /// <summary>
    /// Gets the type name of the command.
    /// </summary>
    string CommandTypeName { get; }

    /// <summary>
    /// Gets the version number of the command schema.
    /// </summary>
    int Version { get; }

    /// <summary>
    /// Gets the date and time in UTC when the command occurred.
    /// </summary>
    DateTime OccurredOnUtc { get; }

    /// <summary>
    /// Gets the unique identifier of the command instance.
    /// </summary>
    Guid CommandId { get; }

    /// <summary>
    /// Gets the unique identifier for the aggregate associated with the domain command.
    /// </summary>
    string AggregateIdentifier { get; }
    
    /// <summary>
    /// Validates the command according to domain rules.
    /// </summary>
    /// <param name="throwOnError">
    /// If <c>true</c>, throws an exception when validation errors are found;
    /// if <c>false</c>, returns the errors without throwing.
    /// </param>
    /// <returns>
    /// An enumerable collection of domain errors found during validation.
    /// An empty collection indicates the command is valid.
    /// </returns>
    IEnumerable<IDomainError> Validate(bool throwOnError);
}