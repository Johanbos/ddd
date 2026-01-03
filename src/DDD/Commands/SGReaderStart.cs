using DDD.Commands.Base;
using DDD.ValueObjects;

namespace DDD.Commands;

public class SGReaderStart : BaseCommand
{
    public required LotId LotId { get; init; }

    public override IEnumerable<DomainError> Validate(bool throwOnError = true)
    {
        List<DomainError> errors = [];
        errors.AddRange(LotId.Validate());

        if (throwOnError && errors.Count != 0)
        {
            var aggregateException = new AggregateException(DomainError.ValidationFailed);
            aggregateException.Data["Errors"] = errors;
            throw aggregateException;
        }

        return errors;
    }
}