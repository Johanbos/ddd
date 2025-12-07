namespace DDD;

public interface IStringValueObject
{
    string Value { get; }
    bool Equals(StringValueObject? other);
    bool Equals(object? obj);
    int GetHashCode();
    string ToString();
}
