
namespace DDD;

public abstract class StringValueObject : IStringValueObject, IEquatable<StringValueObject>
{
    public string Value { get; protected set; } = string.Empty;

    public override bool Equals(object? obj) => Equals(obj as StringValueObject);

    public bool Equals(StringValueObject? other) => other is not null && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}