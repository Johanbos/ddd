using System.Text.RegularExpressions;
using DDD.ValueObjects.Base;

namespace DDD.ValueObjects;

public partial class LotId : BaseValueObject<LotId>
{
    [GeneratedRegex(@"^lot-\d{3}$", RegexOptions.IgnoreCase, "en-NL")]
    private static partial Regex LotIdPattern();

    public string Value
    {
        get;
        init { field = value.Trim().ToLowerInvariant(); }
    }
    
    public LotId(string value = "") : base()
    {
        Value = value;  
    }

    public override IEnumerable<DomainError> Validate(string propertyName = "LotId")
    {
        List<DomainError> errors = [];
        if (string.IsNullOrWhiteSpace(Value))
        {
            errors.Add(new DomainError { Code = DomainError.CannotBeEmpty, PropertyName = propertyName });
        }
        else if (!LotIdPattern().IsMatch(Value))
        {
            errors.Add(new DomainError { Code = DomainError.InvalidPattern, PropertyName = propertyName });
        }

        return errors;
    }

    public override bool Equals(LotId? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LotId);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}