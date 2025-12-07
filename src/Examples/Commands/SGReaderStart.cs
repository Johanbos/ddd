using DDD;
using DDD.ValueObjects;

namespace Examples.Application;

public class SGReaderStart : DomainCommand
{
    public required string LotId { get; init; }

    public SGReaderStart() : base(1)
    {
    }

    public override IEnumerable<IDomainError> Validate(bool throwOnError)
    {
        List<IDomainError> errors = [];
        errors.AddRange(DDD.ValueObjects.LotId.Validate(LotId, false, out string _));

        if (throwOnError && errors.Count != 0)
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }

        return errors;
    }
}