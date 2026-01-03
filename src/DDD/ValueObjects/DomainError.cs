
using DDD.ValueObjects.Base;

namespace DDD.ValueObjects;

public class DomainError : BaseValueObject<DomainError>
{
    public string Code
    {
        get;
        init { field = value.Trim(); }
    }

    public string PropertyName { get; init; }

    public const string ValidationFailed = "ValidationFailed ";
    public const string CannotBeEmpty = "CannotBeEmpty";
    public const string ExceedsMaxLength = "ExceedsMaxLength";
    public const string BelowMinLength = "BelowMinLength";
    public const string InvalidPattern = "InvalidPattern";
    public const string UnknownError = "UnknownError";

    public DomainError(string Code = "", string PropertyName = "") : base()
    {
        this.Code = Code;
        this.PropertyName = PropertyName;   
    }

    public override IEnumerable<DomainError> Validate(string propertyName = "")
    {
        List<DomainError> errors = [];
        if (string.IsNullOrWhiteSpace(Code))
        {
            errors.Add(new DomainError { Code = CannotBeEmpty, PropertyName = propertyName });
        }

        return errors;
    }

    public override bool Equals(DomainError? other)
    {
        return other is not null && Code.Equals(other.Code);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as DomainError);
    }

    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }

    public override string ToString()
    {
        return Code;
    }
}