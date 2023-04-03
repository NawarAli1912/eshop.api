namespace Domain.Products.ValueObjects;

public record SKU
{
    private const int DefaultLength = 15;

    private SKU(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static SKU? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        if (value.Length != DefaultLength)
        {
            return null;
        }

        return new SKU(value);
    }
}
