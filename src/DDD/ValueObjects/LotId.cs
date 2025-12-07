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

    public static new IEnumerable<IDomainError> Validate(string? value, bool throwOnError, out string formattedValue)
    {
        List<DomainError> errors = [];
        if (string.IsNullOrWhiteSpace(value))
        {
            formattedValue = string.Empty;
            errors.Add(new DomainError { Code = DomainError.CannotBeEmpty, ValueName = "LotId" });
        }
        else
        {
            formattedValue = value.Trim().ToLowerInvariant();

            if (!Regex.IsMatch(formattedValue, Pattern, RegexOptions.IgnoreCase))
                errors.Add(new DomainError { Code = DomainError.InvalidFormat, ValueName = "LotId" });
        }

        if (errors.Count != 0 && throwOnError)
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

    public static bool TryCreate(string value, out LotId? lotId, out IEnumerable<IDomainError> errors)
    {
        errors = Validate(value, false, out string formattedValue);
        if (errors.Any())
        {
            lotId = null;
            return false;
        }

        lotId = new LotId(formattedValue);
        return true;
    }
}