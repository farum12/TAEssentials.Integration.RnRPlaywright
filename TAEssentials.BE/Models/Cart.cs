namespace TAEssentials.BE.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<CartItem>? Items { get; set; }
    public string? AppliedCouponCode { get; set; }
    public double Subtotal { get; set; }
    public double DiscountAmount { get; set; }
    public double TotalPrice { get; set; }
    public int TotalItems { get; set; }
    public DateTime LastUpdated { get; set; }
}
