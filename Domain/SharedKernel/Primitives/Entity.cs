namespace Domain.SharedKernel.Primitives;
public abstract class Entity<T> : IEquatable<Entity<T>>
    where T : notnull
{
    public T Id { get; private set; }

    protected Entity(T id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<T> entity
            && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<T>? other)
    {
        return Equals(other);
    }

    public static bool operator ==(
        Entity<T> left,
        Entity<T> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(
        Entity<T> left,
        Entity<T> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}
