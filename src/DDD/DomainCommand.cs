namespace DDD;

[Serializable]
public abstract class DomainCommand(int version) : IDomainCommand
{
    public string CommandTypeName => GetType().FullName ?? string.Empty;
    public int Version { get; init; } = version;
    public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    public Guid CommandId { get; init; } = Guid.NewGuid();
    public string AggregateIdentifier { get; init; } = string.Empty;

    public abstract IEnumerable<IDomainError> Validate(bool throwOnError);
}