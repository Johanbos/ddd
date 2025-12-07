namespace DDD.ValueObjects;

public interface IDomainError
{
    string Code { get; }
    string ValueName { get; }
    string PropertyName { get; }
}
