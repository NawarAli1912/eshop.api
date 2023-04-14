using Domain.SharedKernel.Primitives;

namespace Domain;
public class Errors
{
    public static class Product
    {
        public static Error NotExists =>
                Error.NotFound("Product.NotExists", "Requested product doesn't exists.");
    }

    public static class Category
    {
        public static Error NotExists =>
                Error.NotFound("Category.NotExists", "Requested category doesn't exists.");
    }
}
