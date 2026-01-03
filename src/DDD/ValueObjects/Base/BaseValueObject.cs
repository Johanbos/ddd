namespace DDD.ValueObjects.Base;

[Serializable]
public abstract class BaseValueObject<T> : IEquatable<T>
    where T : BaseValueObject<T>
{
    public abstract IEnumerable<DomainError> Validate(string propertyName = "");
    
    public abstract bool Equals(T? other);  

    public abstract override bool Equals(object? other);

    public abstract override int GetHashCode();
}