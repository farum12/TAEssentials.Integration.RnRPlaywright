using TAEssentials.BE.Enums;

namespace TAEssentials.BE.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderItem>? Items { get; set; }
    public double TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? TransactionId { get; set; }
    public int? PaymentMethodId { get; set; }
    public int? ShippingAddressId { get; set; }
    public DateTime? ExpiresAt { get; set; }
}
