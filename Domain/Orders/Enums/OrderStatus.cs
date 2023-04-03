namespace Domain.Orders.Enums;

public enum OrderStatus : byte
{
    Pending,
    Approved,
    Canceled,
    Rejected
}
