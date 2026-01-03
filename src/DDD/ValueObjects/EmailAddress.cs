using System;
using System.Text.RegularExpressions;
using DDD.ValueObjects.Base;

namespace DDD.ValueObjects;

public sealed partial class EmailAddress : BaseValueObject<EmailAddress>
{
    [GeneratedRegex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$")]
    private static partial Regex EmailAddressPattern();

    public string Value
    {
        get;
        init { field = value.Trim().ToLowerInvariant(); }
    }

    public EmailAddress(string value = "") : base()
    {
        Value = value;
    }

    public override IEnumerable<DomainError> Validate(string propertyName = "EmailAddress")
    {
        List<DomainError> errors = [];
        if (string.IsNullOrWhiteSpace(Value))
        {
            errors.Add(new DomainError { Code = DomainError.CannotBeEmpty, PropertyName = propertyName });
        }
        else if (!EmailAddressPattern().IsMatch(Value))
        {
            errors.Add(new DomainError { Code = DomainError.InvalidPattern, PropertyName = propertyName });
        }

        return errors;
    }

    public override bool Equals(EmailAddress? other)
    {
        return other is not null && Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EmailAddress);
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
