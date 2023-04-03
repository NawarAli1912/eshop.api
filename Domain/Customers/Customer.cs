using Domain.Customers.ValueObjects;

namespace Domain.Customers;

public class Customer
{
    public CustomerId Id { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string? ProfilePicture { get; private set; }

    private Customer(
        CustomerId id,
        string email,
        string firstName,
        string lastName,
        string? profilePicture)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        ProfilePicture = profilePicture;
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
}