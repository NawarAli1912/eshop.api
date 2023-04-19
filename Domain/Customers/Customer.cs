using Domain.Customers.Entities;
using Domain.Customers.ValueObjects;
using Domain.SharedKernel.Primitives;

namespace Domain.Customers;

public class Customer : AggregateRoot<Guid>
{
    public string Email { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string? ProfilePicture { get; private set; }

    public Cart Cart { get; private set; }

    private Customer(
        CustomerId id,
        string email,
        string firstName,
        string lastName,
        string? profilePicture) : base(id.Value)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
        Cart = Cart.Create(CartId.CreateNew());
    }

    public static Customer Create(
        CustomerId id,
        string email,
        string firstName,
        string lastName,
        string? profilePicture)
    {
        return new(id, email, firstName, lastName, profilePicture);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Customer() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}