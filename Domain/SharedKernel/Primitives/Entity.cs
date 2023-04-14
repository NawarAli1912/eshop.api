namespace Domain.SharedKernel.Primitives;
public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj) =>
        obj is Entity<TId> entity
            && Id.Equals(entity.Id);

    public bool Equals(Entity<TId>? other)
        => Equals(other);

    public static bool operator ==(
        Entity<TId> left,
        Entity<TId> right) =>
         Equals(left, right);

    public static bool operator !=(
        Entity<TId> left,
        Entity<TId> right) =>
        !Equals(left, right);


    public override int GetHashCode() =>
               Id.GetHashCode();

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}
