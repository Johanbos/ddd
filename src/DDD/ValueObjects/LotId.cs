using System.Text.RegularExpressions;

namespace DDD.ValueObjects;

public class LotId : StringValueObject
{
    private const string Pattern = @"^lot-\d{3}$";

    public LotId(string value) : base()
    {
        _ = Validate(value, true, out string formattedValue);
        Value = formattedValue;
    }

    public static IEnumerable<IDomainError> Validate(string? value, bool throwOnError, out string formattedValue)
    {
        List<IDomainError> errors = [];
        formattedValue = value?.Trim().ToLowerInvariant() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(formattedValue))
        {
            errors.Add(new DomainError { Code = DomainError.CannotBeEmpty, ValueObjectName = "LotId" });
        }
        else if (!Regex.IsMatch(formattedValue, Pattern, RegexOptions.IgnoreCase))
        {
            errors.Add(new DomainError { Code = DomainError.InvalidFormat, ValueObjectName = "LotId" });
        }

        if (throwOnError && errors.Count != 0)
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }

        return errors;
    }

    public static bool TryCreate(string value, out LotId? valueObject)
    {
        return TryCreate(value, out valueObject, out _);
    }

    public static bool TryCreate(string value, out LotId? valueObject, out IEnumerable<IDomainError> errors)
    {
        errors = Validate(value, false, out string formattedValue);
        if (errors.Any())
        {
            valueObject = null;
            return false;
        }

        valueObject = new LotId(formattedValue);
        return true;
    }
}