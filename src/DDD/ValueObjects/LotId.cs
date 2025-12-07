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

    public static new IEnumerable<Exception> Validate(string? value, bool throwOnError, out string formattedValue)
    {
        List<Exception> errors = [];
        if (string.IsNullOrWhiteSpace(value))
        {
            formattedValue = string.Empty;
            errors.Add(new ArgumentException("LotId cannot be null or empty.", nameof(value)));
        }
        else
        {
            formattedValue = value.Trim().ToLowerInvariant();

            if (!Regex.IsMatch(formattedValue, Pattern, RegexOptions.IgnoreCase))
                errors.Add(new ArgumentException($"LotId must match the pattern 'lot-###' where # is a digit. Got: {value}", nameof(value)));
        }

        if (errors.Count != 0 && throwOnError)
            throw new AggregateException("Invalid LotId value.", errors);

        return errors;
    }

    public static bool TryCreate(string value, out LotId? valueObject)
    {
        return TryCreate(value, out valueObject, out _);
    }

    public static bool TryCreate(string value, out LotId? lotId, out IEnumerable<Exception> errors)
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
