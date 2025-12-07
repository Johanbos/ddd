
namespace DDD.ValueObjects;

public record DomainError : IDomainError
{
    public required string Code { get; init; }
    public string ValueObjectName { get; init; } = string.Empty;
    public string PropertyName { get; init; } = string.Empty;

    public const string ValidationFailed = "ValidationFailed ";
    public const string CannotBeEmpty = "CannotBeEmpty";
    public const string ExceedsMaxLength = "ExceedsMaxLength";
    public const string BelowMinLength = "BelowMinLength";
    public const string InvalidFormat = "InvalidFormat";
}