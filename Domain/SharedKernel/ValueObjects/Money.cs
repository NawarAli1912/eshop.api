using Domain.SharedKernel.Enums;
using Domain.SharedKernel.Primitives;

namespace Domain.SharedKernel.ValueObjects;
public class Money : ValueObject
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        return new(amount, currency);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}