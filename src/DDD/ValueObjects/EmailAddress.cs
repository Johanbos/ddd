using System;
using System.Text.RegularExpressions;

namespace DDD.ValueObjects;

public class EmailAddress : StringValueObject
{
    private const string Pattern = "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$";

    public EmailAddress(string value) : base()
    {
        _ = Validate(value, true, out string formattedValue);
        Value = formattedValue;
    }

    public static new IEnumerable<IDomainError> Validate(string? value, bool throwOnError, out string formattedValue)
    {
        List<DomainError> errors = [];
        formattedValue = value?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(formattedValue))
        {
            errors.Add(new DomainError { Code = DomainError.CannotBeEmpty, ValueName = "EmailAddress" });
        }
        else if (!Regex.IsMatch(formattedValue, Pattern))
        {
            errors.Add(new DomainError { Code = DomainError.InvalidFormat, ValueName = "EmailAddress" });
        }

        if (throwOnError && errors.Count != 0)
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }
        return errors;
    }

    public static bool TryCreate(string value, out EmailAddress? valueObject)
    {
        return TryCreate(value, out valueObject, out _);
    }

    public static bool TryCreate(string value, out EmailAddress? valueObject, out IEnumerable<IDomainError> errors)
    {
        errors = Validate(value, false, out string formattedValue);
        if (errors.Any())
        {
            valueObject = null;
            return false;
        }

        valueObject = new EmailAddress(formattedValue);
        return true;
    }
}
