namespace Domain.SharedKernel.Primitives;
public abstract class AggregateRoot<T> : Entity<T>
    where T : notnull
{
    protected AggregateRoot(T id) : base(id)
    {
    }

    #region ef
    protected AggregateRoot() { }
    #endregion
}
