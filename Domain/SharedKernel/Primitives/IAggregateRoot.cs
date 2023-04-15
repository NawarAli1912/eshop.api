namespace Domain.SharedKernel.Primitives;
public interface IAggregateRoot
{
    void RaiseDomainEvent(IDomainEvent domainEvent);

    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
