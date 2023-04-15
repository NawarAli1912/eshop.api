using Nest;

namespace Domain.SharedKernel.Abstraction.ElasticTypes;

public class Product
{
    [Text(Name = "product_id")]
    public required string ProudctId { get; set; }

    [Text(Name = "product_name")]
    public required string Name { get; set; }

    [Text(Name = "product_description")]
    public required string Description { get; set; }

    [Number(Name = "product_price")]
    public decimal Price { get; set; }

    [Text(Name = "product_categories")]
    public required string Categories { get; set; }

    [Number(Name = "product_rating")]
    public double AverageRating { get; set; }
}
