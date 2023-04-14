using Domain.SharedKernel.Primitives;

namespace Domain.SharedKernel.ValueObjects;
public class AverageRating : ValueObject
{
    public double Value { get; private set; }

    public int RatingCount { get; private set; }

    private AverageRating(double value, int ratingCount)
    {
        Value = value;
        RatingCount = ratingCount;
    }

    public static AverageRating Create(double value, int ratingCount)
    {
        return new(value, ratingCount);
    }

    public void AddRating(int rating)
    {
        if (rating < 0 || rating > 5)
        {
            throw new ArgumentException("Rating should be between 0 and 5");
        }

        Value = (Value / RatingCount + rating) / (RatingCount + 1);
        RatingCount++;
    }

    public void RemoveRating(int rating)
    {
        if (rating < 0 || rating > 5)
        {
            throw new ArgumentException("Rating should be between 0 and 5");
        }

        if (RatingCount - 1 == 0)
        {
            Value = 0.0;
            return;
        }

        Value = (Value / RatingCount - rating) / (RatingCount - 1);
        RatingCount--;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return RatingCount;
    }
}
