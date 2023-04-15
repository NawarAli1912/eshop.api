using Domain.SharedKernel.Primitives;

namespace Domain.Products.DomainEvents;
public sealed record ProductCreatedDomainEvent(
    string Id,
    string Name,
    string Description,
    decimal Price
    ) : IDomainEvent;
