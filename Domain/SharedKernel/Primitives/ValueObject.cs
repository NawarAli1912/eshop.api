namespace Domain.SharedKernel.Primitives;
public abstract class ValueObject : IEquatable<ValueObject>
{
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());
    }
    public override int GetHashCode() =>
        GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);

    public static bool operator ==(ValueObject left, ValueObject right) =>
            Equals(left, right);

    public static bool operator !=(ValueObject left, ValueObject right) =>
            !Equals(left, right);


    public abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return Equals((object?)other);
    }
}
