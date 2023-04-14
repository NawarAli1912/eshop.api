using Domain.SharedKernel.Primitives;

namespace Domain.Products.ValueObjects;

public class SKU : ValueObject
{
    private const int DefaultLength = 8;

    private SKU(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Result<SKU> Create(string value)
    {
        var errors = new List<Error>();
        if (string.IsNullOrEmpty(value))
        {
            errors.Add(Error.Validation("SKU.Empty", "SKU shouldn't be empty."));
        }

        if (value.Length != DefaultLength)
        {
            errors.Add(Error.Validation("SKU.InvalidLength", "SKU should be 8 characters long."));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new SKU(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
