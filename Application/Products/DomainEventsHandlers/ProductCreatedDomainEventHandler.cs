using Domain.Products.DomainEvents;
using Domain.SharedKernel.Abstraction.ElasticTypes;
using MediatR;
using Nest;

namespace Application.Products.DomainEventsHandlers;

internal sealed class ProductCreatedDomainEventHandler
    : INotificationHandler<ProductCreatedDomainEvent>
{
    private readonly IElasticClient _elasticClient;

    public ProductCreatedDomainEventHandler(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var response = await _elasticClient.IndexDocumentAsync(new Product
        {
            ProudctId = notification.Id,
            Name = notification.Name,
            Description = notification.Description,
            Price = notification.Price,
            AverageRating = 0.0,
            Categories = string.Empty
        }, cancellationToken);

        if (!response.IsValid)
        {
            throw new Exception(response.DebugInformation);
        }
    }
}