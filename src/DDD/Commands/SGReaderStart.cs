using DDD.Commands.Base;
using DDD.ValueObjects;

namespace DDD.Commands;

public class SGReaderStart : BaseCommand
{
    public required LotId LotId { get; init; }

    public override IEnumerable<DomainError> Validate()
    {
        return LotId.Validate();
    }

    // Map type to queue
}