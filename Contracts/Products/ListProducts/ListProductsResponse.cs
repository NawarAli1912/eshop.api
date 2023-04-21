using Domain.Products;

namespace Contracts.Products.ListProducts;

public record ListProductItemsResponse
{
    public ListProductItemsResponse(
        string id,
        string name,
        string description,
        decimal price,
        double rating,
        List<string> reviews)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Rating = rating;
        Reviews = reviews;
    }

    public string Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public double Rating { get; private set; }

    public List<string> Reviews { get; private set; } = new();


    public static IEnumerable<ListProductItemsResponse> Create(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            yield return new ListProductItemsResponse(
                product.Id.ToString(),
                product.Name,
                product.Description,
                product.Price.Amount,
                product.AverageRating.Value,
                product.Reviews.Select(r => r.Comment).ToList());
        }
    }
}
