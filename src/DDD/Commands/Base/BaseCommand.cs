using DDD.ValueObjects;

namespace DDD.Commands.Base;

[Serializable]
public abstract class BaseCommand(int version = 1)
{
    public int Version { get; init; } = version;
    public Guid CommandId { get; init; } = Guid.NewGuid(); // NewId.NextGuid();
    public abstract IEnumerable<DomainError> Validate(bool throwOnError = true);
}